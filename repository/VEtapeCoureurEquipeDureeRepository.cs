using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class VEtapeCoureurEquipeDureeRepository
{
    private readonly AppDbContext _context;

    public VEtapeCoureurEquipeDureeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<VEtapeCoureurEquipeDuree> FindAll()
    {
        return _context._v_etape_coureur_equipe_duree?.ToList()?? new List<VEtapeCoureurEquipeDuree>();
    }
    public IEnumerable<IGrouping<string, VEtapeCoureurEquipeDuree>> FindAllByIdEquipe(string? idEquipe)
    {
        var query = _context._v_etape_coureur_equipe_duree
            .Include(x=> x.Coureur)
            .Include(x=> x.Equipes)
            .Include(x=> x.Etapes)
            .Where(c => c.IdEquipe == idEquipe)
            .ToList(); // Exécutez la requête et convertissez-la en liste

        if (query == null || !query.Any())
        {
            return new List<IGrouping<string, VEtapeCoureurEquipeDuree>>(); // Retourne une liste vide de groupes si la requête est null ou vide
        }
    #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return query.GroupBy(c => c.IdEtape).ToList();
    #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }


    public void Add(VEtapeCoureurEquipeDuree v_etape_coureur_equipe_duree)
    {
        if (v_etape_coureur_equipe_duree == null)
        {
            throw new ArgumentNullException(nameof(v_etape_coureur_equipe_duree));
        }

        _context._v_etape_coureur_equipe_duree.Add(v_etape_coureur_equipe_duree);
        _context.SaveChanges();
    }

    public void Update(VEtapeCoureurEquipeDuree  v_etape_coureur_equipe_duree)
    {
        if (v_etape_coureur_equipe_duree == null)
        {
            throw new ArgumentNullException(nameof(v_etape_coureur_equipe_duree));
        }

        _context._v_etape_coureur_equipe_duree.Update(v_etape_coureur_equipe_duree);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var v_etape_coureur_equipe_dureeToDelete = _context._v_etape_coureur_equipe_duree.Find();
        if (v_etape_coureur_equipe_dureeToDelete != null)
        {
            _context._v_etape_coureur_equipe_duree.Remove(v_etape_coureur_equipe_dureeToDelete);
            _context.SaveChanges();
        }
    }
}