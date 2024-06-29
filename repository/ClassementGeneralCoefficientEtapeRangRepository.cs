using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class ClassementGeneralCoefficientEtapeRangRepository
{
    private readonly AppDbContext _context;

    public ClassementGeneralCoefficientEtapeRangRepository(AppDbContext context)
    { 
        _context = context;
    }
    public List<ClassementGeneralCoefficientEtapeRang> FindAll()
    {
        return _context._classement_general_coefficient_etape_rang?.ToList()?? new List<ClassementGeneralCoefficientEtapeRang>();
    }

    public void Add(ClassementGeneralCoefficientEtapeRang classement_general_coefficient_etape_rang)
    {
        if (classement_general_coefficient_etape_rang == null)
        {
            throw new ArgumentNullException(nameof(classement_general_coefficient_etape_rang));
        }

        _context._classement_general_coefficient_etape_rang.Add(classement_general_coefficient_etape_rang);
        _context.SaveChanges();
    }

    public void Update(ClassementGeneralCoefficientEtapeRang  classement_general_coefficient_etape_rang)
    {
        if (classement_general_coefficient_etape_rang == null)
        {
            throw new ArgumentNullException(nameof(classement_general_coefficient_etape_rang));
        }

        _context._classement_general_coefficient_etape_rang.Update(classement_general_coefficient_etape_rang);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var classement_general_coefficient_etape_rangToDelete = _context._classement_general_coefficient_etape_rang.Find();
        if (classement_general_coefficient_etape_rangToDelete != null)
        {
            _context._classement_general_coefficient_etape_rang.Remove(classement_general_coefficient_etape_rangToDelete);
            _context.SaveChanges();
        }
    }

    public List<ClassementGeneralCoefficientEtapeRang> GetPointsParEtape(){
        var classementParEquipe = _context._classement_general_coefficient_etape_rang
            .GroupBy(x => new { x.Etape, x.Equipe , x.Coefficient })
            .Select(g => new ClassementGeneralCoefficientEtapeRang{
                Etape = g.Key.Etape,
                Equipe = g.Key.Equipe,
                Coefficient = g.Key.Coefficient,
                PointCoefficient = g.Sum(x => x.PointCoefficient)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }

    public List<ClassementGeneralCoefficientEtapeRang> GetPointsParEtapeCoureur(string idEtape){
        var classementParEquipe = _context._classement_general_coefficient_etape_rang
            .Where(x => x.IdEtape == idEtape)
            .GroupBy(x => new {
                 x.Etape, x.Equipe, x.Coureur , x.Genre , x.HeureDepart , x.HeureArrive ,  x.TempsPenalite , x.DureeTotale
                 })
            .Select(g => new ClassementGeneralCoefficientEtapeRang{
                Etape = g.Key.Etape,
                Equipe = g.Key.Equipe,
                Coureur = g.Key.Coureur,
                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }
    public List<ClassementGeneralCoefficientEtapeRang> FindAllByIdEtape(string id_etape){
        return _context._classement_general_coefficient_etape_rang?
                .Where(i => i.IdEtape == id_etape)
                .OrderByDescending(x => x.Point)
                .ToList()?? new List<ClassementGeneralCoefficientEtapeRang>();
        }


}