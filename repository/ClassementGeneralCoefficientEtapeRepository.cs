using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class ClassementGeneralCoefficientEtapeRepository
{
    private readonly AppDbContext _context;

    public ClassementGeneralCoefficientEtapeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<ClassementGeneralCoefficientEtape> FindAll()
    {
        return _context._classement_general_coefficient_etape?.ToList()?? new List<ClassementGeneralCoefficientEtape>();
    }

    public void Add(ClassementGeneralCoefficientEtape classement_general_coefficient_etape)
    {
        if (classement_general_coefficient_etape == null)
        {
            throw new ArgumentNullException(nameof(classement_general_coefficient_etape));
        }

        _context._classement_general_coefficient_etape.Add(classement_general_coefficient_etape);
        _context.SaveChanges();
    }

    public void Update(ClassementGeneralCoefficientEtape  classement_general_coefficient_etape)
    {
        if (classement_general_coefficient_etape == null)
        {
            throw new ArgumentNullException(nameof(classement_general_coefficient_etape));
        }

        _context._classement_general_coefficient_etape.Update(classement_general_coefficient_etape);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var classement_general_coefficient_etapeToDelete = _context._classement_general_coefficient_etape.Find();
        if (classement_general_coefficient_etapeToDelete != null)
        {
            _context._classement_general_coefficient_etape.Remove(classement_general_coefficient_etapeToDelete);
            _context.SaveChanges();
        }
        
    }


     public List<ClassementGeneralCoefficientEtape> GetPointsParEtape(){
        var classementParEquipe = _context._classement_general_coefficient_etape
            .GroupBy(x => new { x.IdEtape , x.Etape , x.Coefficient , x.Longueur})
            .Select(g => new ClassementGeneralCoefficientEtape {
                IdEtape = g.Key.IdEtape,
                Etape = g.Key.Etape,
                Coefficient = g.Key.Coefficient,
                Longueur = g.Key.Longueur,
                Point = g.Sum(e=> e.Point)
            })
            .OrderByDescending(e0 => e0.Point).ToList();
        return classementParEquipe;
    }
}