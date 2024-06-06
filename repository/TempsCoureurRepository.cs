using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class TempsCoureurRepository
{
    private readonly AppDbContext _context;

    public TempsCoureurRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<TempsCoureur> FindAll()
    {
        return _context._temps_coureur?.ToList()?? new List<TempsCoureur>();
    }

    public void Add(TempsCoureur temps_coureur)
    {
        if (temps_coureur == null)
        {
            throw new ArgumentNullException(nameof(temps_coureur));
        }

        _context._temps_coureur.Add(temps_coureur);
        _context.SaveChanges();
    }

    public void Update(TempsCoureur  temps_coureur)
    {
        if (temps_coureur == null)
        {
            throw new ArgumentNullException(nameof(temps_coureur));
        }

        _context._temps_coureur.Update(temps_coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var temps_coureurToDelete = _context._temps_coureur.Find(id);
        if (temps_coureurToDelete != null)
        {
            _context._temps_coureur.Remove(temps_coureurToDelete);
            _context.SaveChanges();
        }
    }

    public void InsertionTempsCoureur(string? id_etape, string coureurId, DateTime heure_debut, DateTime heure_fin){
        TempsCoureur tempsCoureur = new TempsCoureur{
            IdEtape = id_etape,
            IdCoureur = coureurId,
            HeureDepart = heure_debut,
            HeureArrive = heure_fin 
        };
        _context._temps_coureur.Add(tempsCoureur);
        _context.SaveChanges();
    }

    public List<TempsCoureur>  finById(string id)
    {
        return _context._temps_coureur.Where(x => x.IdEtape == id).ToList();
    }

    // public void InsertionTempsCoureur(string? id_etape, string[] coureurId, DateTime[] heure_debut, DateTime[] heure_fin)
    // {
    //     for (int i = 0; i < coureurId.Count(); i++){
    //         TempsCoureur tempsCoureur = new TempsCoureur
    //         {
    //             IdEtape = id_etape,
    //             IdCoureur = coureurId[i],
    //             HeureDepart = heure_debut[i],
    //             HeureArrive = heure_fin[i] 
    //         };
    //         _context._temps_coureur.Add(tempsCoureur);
    //     }
    //     _context.SaveChanges();
    // }
}