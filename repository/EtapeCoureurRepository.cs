using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using Npgsql;

public class EtapeCoureurRepository
{
    private readonly AppDbContext _context;

    public EtapeCoureurRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<EtapeCoureur> FindAll()
    {
        return _context._etape_coureur?.ToList()?? new List<EtapeCoureur>();
    }

    public void Add(EtapeCoureur etape_coureur)
    {
        if (etape_coureur == null)
        {
            throw new ArgumentNullException(nameof(etape_coureur));
        }

        _context._etape_coureur.Add(etape_coureur);
        _context.SaveChanges();
    }

    public void AddAllCoureur(string id_etape, string[] id_coureur)
    {
        foreach (var id in id_coureur){
            EtapeCoureur etapeCoureur = new EtapeCoureur(id_etape,id);
            _context._etape_coureur.Add(etapeCoureur); 
        }
        _context.SaveChanges();
    }

    public void Update(EtapeCoureur  etape_coureur)
    {
        if (etape_coureur == null)
        {
            throw new ArgumentNullException(nameof(etape_coureur));
        }

        _context._etape_coureur.Update(etape_coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var etape_coureurToDelete = _context._etape_coureur.Find(id);
        if (etape_coureurToDelete != null)
        {
            _context._etape_coureur.Remove(etape_coureurToDelete);
            _context.SaveChanges();
        }
    }

    public List<EtapeCoureur> GetCoureursByEtape(string etapeId)
    {
        var etapeCoureurs = _context._etape_coureur?
                       .Include(x => x.Coureur)
                       .Where(x => x.IdEtape == etapeId)
                       .ToList();
        ;
        return etapeCoureurs;
    }

    public double GetNombreEquipeinserer(string id_etape)
    {
        var etapeCounts = _context._etape_coureur
        .Where(x => x.IdEtape == id_etape)
        .GroupBy(x => x.IdEtape)
        .Select(g => new
        {
            IdEtape = g.Key,
            NombreIdCoureur = g.Count() // Compte le nombre d'occurrences de id_coureur pour chaque groupe
        })
        .FirstOrDefault();

    return etapeCounts?.NombreIdCoureur ?? 0;
    }
    public List<EtapeCoureur> GetEtapeCoureurNotInTempsCoureur(string etapeId)
    {
        string query = @"
            SELECT * FROM etape_coureur 
            WHERE id_etape = @etapeId
              AND id_coureur NOT IN (SELECT id_coureur FROM temps_coureur WHERE id_etape = @etapeId) ";

        var etapeCoureurs = _context._etape_coureur
                                    .FromSqlRaw<EtapeCoureur>(query, new NpgsqlParameter("@etapeId", etapeId))
                                    .ToList();

        foreach (var item in etapeCoureurs)
        {
            Console.WriteLine(item.IdCoureur);
        }
        return etapeCoureurs;
    }
}