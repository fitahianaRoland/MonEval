-- FROM classement_par_equipe;
CREATE OR REPLACE VIEW classement AS
WITH penalites_par_etape_equipe AS (
    SELECT
        id_etape,
        id_equipe,
        SUM(temps_penalite::interval) AS somme_temps_penalites -- Somme des temps de pénalité convertis en INTERVAL
    FROM
        etape_equipe_penalite
    GROUP BY
        id_etape,
        id_equipe
),
classement AS (
    SELECT
        t.id_etape,
        t.id_coureur,
        c.id_equipe,
        t.heure_depart,
        t.heure_arrive,
        t.heure_arrive - t.heure_depart AS duree,
        COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') AS temps_penalite, -- Utiliser la somme des pénalités
        DENSE_RANK() OVER(PARTITION BY t.id_etape ORDER BY (t.heure_arrive + COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds')) - t.heure_depart) AS rang
    FROM
        temps_coureur t
    JOIN
        coureur c ON t.id_coureur = c.id
    LEFT JOIN
        penalites_par_etape_equipe pe ON t.id_etape = pe.id_etape AND c.id_equipe = pe.id_equipe
)
SELECT
    classement.id_etape,
    classement.id_coureur,
    classement.id_equipe,
    classement.heure_depart,
    classement.heure_arrive,
    classement.duree + classement.temps_penalite AS duree_totale, -- Ajoute la pénalité à la durée totale
    classement.rang,
    classement.temps_penalite,
    COALESCE(points.point, 0) AS point
FROM
    classement
LEFT JOIN
    points ON classement.rang = points.rang
ORDER BY
    classement.id_etape,
    duree_totale;
-------------------------------------------
--Classement general
CREATE OR REPLACE VIEW classement_general_view AS
    SELECT 
    c.*, 
    et.nom as etape,
    et.longueur ,
    et.nombre_coureur_equipe,
    et.rang_etape,
    co.nom as coureur,
    co.numero_dossard,
    co.date_de_naissance,
    co.id_genre,
    g.nom as genre,
    eq.nom as equipe
FROM 
    classement c
JOIN 
    coureur co ON c.id_coureur = co.id
LEFT JOIN 
    genre g ON co.id_genre = g.id
LEFT JOIN 
    equipe eq ON c.id_equipe = eq.id
LEFT JOIN 
    etape et ON c.id_etape = et.id;

select etape,equipe,sum(point) from classement_general_view group by etape,equipe;


CREATE OR REPLACE FUNCTION f_coureur_categorie()
RETURNS VOID AS $$
DECLARE
    id_categorie_g VARCHAR;
    id_categorie_a VARCHAR;
    c_age INTEGER;
    coureur_row RECORD;
BEGIN

    FOR coureur_row IN 
        SELECT coureur.*, genre.nom as g_nom 
        FROM coureur 
        JOIN genre ON coureur.id_genre = genre.id 
        WHERE coureur.id NOT IN (SELECT id_coureur FROM categorie_coureur)
    LOOP
        IF coureur_row.g_nom = 'M' THEN
            id_categorie_g := (SELECT id FROM categorie WHERE nom = 'Homme');
        ELSE
            id_categorie_g := (SELECT id FROM categorie WHERE nom = 'Femme');
        END IF;

        SELECT EXTRACT(YEAR FROM AGE(coureur_row.date_de_naissance)) INTO c_age;

        IF c_age < 18 THEN
            id_categorie_a := (SELECT id FROM categorie WHERE nom = 'Junior');
        ELSE
            id_categorie_a := (SELECT id FROM categorie WHERE nom = 'Senior');
        END IF;

        INSERT INTO categorie_coureur (id_coureur, id_categorie) VALUES (coureur_row.id, id_categorie_g);
        INSERT INTO categorie_coureur (id_coureur, id_categorie) VALUES (coureur_row.id, id_categorie_a);
    END LOOP;
    
    RETURN;
END;
$$ LANGUAGE plpgsql;


-- CLASSEMENT CATEGORIE
CREATE OR REPLACE VIEW classement_categorie AS
WITH penalites_par_etape_equipe AS (
    SELECT
        id_etape,
        id_equipe,
        SUM(temps_penalite) AS somme_temps_penalites -- Somme des temps de pénalité convertis en INTERVAL
    FROM
        etape_equipe_penalite
    GROUP BY
        id_etape,
        id_equipe
),
classement AS (
    SELECT
        t.id_etape,
        t.id_coureur,
        c.id_equipe,
        cc.id_categorie,
        t.heure_depart,
        t.heure_arrive,
        (t.heure_arrive + COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') - t.heure_depart) AS duree,
        DENSE_RANK() OVER(PARTITION BY t.id_etape, cc.id_categorie ORDER BY (t.heure_arrive + COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') - t.heure_depart)) AS rang,
        COALESCE(pe.somme_temps_penalites, INTERVAL '0 seconds') AS temps_penalite
    FROM
        temps_coureur t
    JOIN
        coureur c ON t.id_coureur = c.id
    JOIN
        categorie_coureur cc ON t.id_coureur = cc.id_coureur
    LEFT JOIN
        penalites_par_etape_equipe pe ON t.id_etape = pe.id_etape AND c.id_equipe = pe.id_equipe
)
SELECT
    classement.id_etape,
    classement.id_coureur,
    classement.id_equipe,
    classement.id_categorie,
    classement.heure_depart,
    classement.heure_arrive,
    classement.duree,
    classement.rang,
    COALESCE(points.point, 0) AS point,
    classement.temps_penalite
FROM
    classement
LEFT JOIN
    points ON classement.rang = points.rang;


CREATE OR REPLACE VIEW classement_general_categorie_view AS
    SELECT 
    c.*, 
    et.nom as etape,
    et.longueur ,
    et.nombre_coureur_equipe,
    et.rang_etape,
    co.nom as coureur,
    co.numero_dossard,
    co.date_de_naissance,
    co.id_genre,
    g.nom as genre,
    eq.nom as equipe,
    cat.nom as nom_categorie
FROM 
    classement_categorie c
LEFT JOIN 
    coureur co ON c.id_coureur = co.id
LEFT JOIN 
    genre g ON co.id_genre = g.id
LEFT JOIN 
    equipe eq ON c.id_equipe = eq.id
LEFT JOIN 
    etape et ON c.id_etape = et.id
LEFT JOIN 
    categorie cat ON c.id_categorie = cat.id;

-- select id_categorie,equipe,sum(point) from classement_general_categorie_view 
-- where id_categorie = 'CAT003' 
-- group by id_categorie,equipe;


-- JOINTURE ETAPE COUREUR EQUIPE
CREATE OR REPLACE VIEW v_etape_coureur_equipe AS
SELECT 
    ec.*,
    c.nom, 
    c.numero_dossard, 
    c.date_de_naissance, 
    c.id_genre, 
    c.id_equipe 
FROM etape_coureur AS ec
JOIN coureur as c ON ec.id_coureur = c.id;

-- JOINTURE ETAPE COUREUR EQUIPE DUREE
CREATE OR REPLACE VIEW v_etape_coureur_equipe_duree AS
SELECT 
    vece.*,
    vc.duree_totale::time as duree
FROM v_etape_coureur_equipe AS vece
LEFT JOIN classement AS vc 
ON vece.id_etape = vc.id_etape 
AND vece.id_equipe = vc.id_equipe 
AND vece.id_coureur = vc.id_coureur;


-- select * from v_etape_coureur_equipe_duree where id_equipe = 'EQP001' AND id_etape = ''

-- select id_etape,id_equipe,id_coureur,duree from v_etape_coureur_equipe_duree where id_equipe = 'EQP001'
-- group by id_etape,id_equipe,id_coureur,duree;
    -- _______________________________________________________________________________________

-- --Insert etape 
-- INSERT INTO etape (nom,longueur,nombre_coureur_equipe,rang_etape) 
--     SELECT DISTINCT ON (e.etape, e.rang)
--         e.etape,e.longueur,e.nb_coureur,e.rang 
--     FROM temp_etape e;
-- --Insert coureur depart 
-- INSERT INTO coureur_depart (id_etape, heure_depart)
--     SELECT DISTINCT ON (te.date_depart, te.heure_depart)
--         e.id as id_etape, (te.date_depart + te.heure_depart) as date_heure_depart
--         FROM temp_etape te
--         JOIN etape e ON e.nom = te.etape and e.rang_etape = te.rang;
-- --------------------------------------Temp resultat---------------------------------------------
-- --Insert genre
-- INSERT INTO genre (nom) 
--     SELECT DISTINCT ON (genre)
--         genre 
--         FROM temp_resultat;

-- --Insert equipe
-- INSERT INTO equipe (nom,login,mot_de_passe) 
-- SELECT DISTINCT ON (equipe)
-- equipe, equipe || '@gmail.com' AS email, '1234' AS password
-- FROM temp_resultat;
-- --Insert coureur
-- INSERT INTO coureur(nom, numero_dossard, date_de_naissance, id_genre, id_equipe)  
--     SELECT DISTINCT ON (nom, numero_dossard)
--          tr.nom,tr.numero_dossard,tr.date_de_naissance,g.id as id_genre,e.id as id_equipe 
--         FROM temp_resultat tr
--             JOIN equipe e ON tr.equipe = e.nom
--             JOIN genre g ON tr.genre = g.nom;

-- --Insert etape_coureur
-- INSERT INTO etape_coureur(id_etape, id_coureur)  
--     SELECT DISTINCT e.id as id_etape,c.id as id_coureur 
--         FROM  temp_resultat tr
--             JOIN etape e ON tr.etape_rang = e.rang_etape
--             JOIN coureur c ON tr.nom = c.nom;


-- --Insert coureur arrive
-- INSERT INTO coureur_arrive (id_etape,id_coureur, heure_arrive)
--     SELECT DISTINCT e.id as id_etape, c.id as id_coureur , tr.arrive as date_heure_arrive
--         FROM temp_resultat tr
--             JOIN etape e ON tr.etape_rang = e.rang_etape
--             JOIN coureur c ON tr.nom = c.nom 
--             JOIN etape_coureur ec ON c.id = ec.id_coureur AND e.id = ec.id_etape;

-- INSERT INTO temps_coureur (id_etape, id_coureur, heure_depart, heure_arrive)
--     SELECT  cd.id_etape, ca.id_coureur, cd.heure_depart, ca.heure_arrive
--         FROM coureur_depart cd
--             JOIN coureur_arrive ca ON cd.id_etape = ca.id_etape ;
-- ---------------------------------------TTemp points--------------------------------------------
-- INSERT INTO points (rang,point)
--     SELECT classement,points 
--         FROM temp_points;

---------------------------------------categorie--------------------------------------------