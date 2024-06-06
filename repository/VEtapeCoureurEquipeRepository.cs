using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class VEtapeCoureurEquipeRepository
{
    private readonly AppDbContext _context;

    public VEtapeCoureurEquipeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<VEtapeCoureurEquipe> FindAll()
    {
        return _context._v_etape_coureur_equipe?.ToList()?? new List<VEtapeCoureurEquipe>();
    }

    public void Add(VEtapeCoureurEquipe v_etape_coureur_equipe)
    {
        if (v_etape_coureur_equipe == null)
        {
            throw new ArgumentNullException(nameof(v_etape_coureur_equipe));
        }

        _context._v_etape_coureur_equipe.Add(v_etape_coureur_equipe);
        _context.SaveChanges();
    }

    public void Update(VEtapeCoureurEquipe  v_etape_coureur_equipe)
    {
        if (v_etape_coureur_equipe == null)
        {
            throw new ArgumentNullException(nameof(v_etape_coureur_equipe));
        }

        _context._v_etape_coureur_equipe.Update(v_etape_coureur_equipe);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var v_etape_coureur_equipeToDelete = _context._v_etape_coureur_equipe.Find();
        if (v_etape_coureur_equipeToDelete != null)
        {
            _context._v_etape_coureur_equipe.Remove(v_etape_coureur_equipeToDelete);
            _context.SaveChanges();
        }
    }

    public double GetNombreEtapeEquipe(string id_equipe , string id_etape)
    {
        var etapeCounts = _context._v_etape_coureur_equipe
        .Where(x => x.IdEquipe == id_equipe  && x.IdEtape == id_etape)
        .GroupBy(x => x.IdEtape)
        .Select(g => new
        {
            IdEtape = g.Key,
            NombreIdCoureur = g.Count() // Compte le nombre d'occurrences de id_coureur pour chaque groupe
        })
        .FirstOrDefault();

    return etapeCounts?.NombreIdCoureur ?? 0;
    }
}