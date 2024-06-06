using dotnetEval.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnetEval;

public class Helper
{

    public static TimeSpan CalculDifferenceHeures(DateTime heureDebut, DateTime heureFin)
    {
        // Vérifier si l'heure de fin est avant l'heure de début (par exemple, si le jour suivant)
        if (heureFin < heureDebut)
        {
            // Ajouter une journée à l'heure de fin pour obtenir une heure valide
            heureFin = heureFin.AddDays(1);
        }

        // Calculer la différence entre les deux heures
        TimeSpan difference = heureFin - heureDebut;

        return difference;
    }
    public void ResetDataBase(AppDbContext context)
    {
        string sql = @"
            DO $$
            DECLARE
                table_record RECORD;
            BEGIN
                -- Tronquer toutes les tables
                FOR table_record IN
                    SELECT table_name
                    FROM information_schema.tables
                    WHERE table_schema = 'public' AND table_type = 'BASE TABLE'
                LOOP
                    EXECUTE format('TRUNCATE TABLE %I RESTART IDENTITY CASCADE', table_record.table_name);
                END LOOP;
            END $$;
        ";
        context.Database.ExecuteSqlRaw(sql);
        context.Database.ExecuteSqlRaw(@"
            DO
            $$
            DECLARE
                seq_name text;
            BEGIN
                FOR seq_name IN SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = 'public' LOOP
                    EXECUTE 'ALTER SEQUENCE ' || seq_name || ' RESTART WITH 1';
                END LOOP;
            END;
            $$;
        ");
        // Ajout d'un utilisateur dans la table btp
        string request = "INSERT INTO administrateur(nom, email, mot_de_passe) VALUES ('ADMIN', 'admin@gmail.com', '12345')";
        context.Database.ExecuteSqlRaw(request);

        string categories = @"INSERT INTO categorie(nom) VALUES
                            ('Junior'),
                            ('Senior'),
                            ('Homme'),
                            ('Femme');";
        context.Database.ExecuteSqlRaw(categories);
        
        context.SaveChanges();
    }
}
