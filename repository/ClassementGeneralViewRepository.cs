using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;
public class ClassementGeneralViewRepository
{
    private readonly AppDbContext _context;

    public ClassementGeneralViewRepository(AppDbContext context){
        _context = context;
    }
    public List<Classement_general_view> FindAll(){
        return _context._classement_general_view?.ToList()?? new List<Classement_general_view>();
    }

    public void Add(Classement_general_view classement_general_view){
        if (classement_general_view == null){
            throw new ArgumentNullException(nameof(classement_general_view));
        }

        _context._classement_general_view.Add(classement_general_view);
        _context.SaveChanges();
    }

    public void Update(Classement_general_view  classement_general_view){
        if (classement_general_view == null){
            throw new ArgumentNullException(nameof(classement_general_view));
        }

        _context._classement_general_view.Update(classement_general_view);
        _context.SaveChanges();
    }

    public void Delete( ){
        var classement_general_viewToDelete = _context._classement_general_view.Find();
        if (classement_general_viewToDelete != null){
            _context._classement_general_view.Remove(classement_general_viewToDelete);
            _context.SaveChanges();
        }
    }
    public List<Classement_general_view> GetClassementGeneral(){
        var classementGeneral = _context._classement_general_view
            .GroupBy(x => new { x.IdCoureur, x.Coureur , x.Equipe , x.Longueur})
            .Select(g => new Classement_general_view{
                IdCoureur = g.Key.IdCoureur,
                Coureur = g.Key.Coureur,
                Equipe = g.Key.Equipe,
                Longueur = g.Key.Longueur,
                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementGeneral;
    }


    public List<Classement_general_view> GetClassementParEquipe(){
        var classementParEquipe = _context._classement_general_view
            .GroupBy(x => new {x.IdEquipe , x.Equipe })
            .Select(g => new Classement_general_view{ 
                IdEquipe = g.Key.IdEquipe,
                Equipe = g.Key.Equipe,
                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }

    public List<Classement_general_view> GetClassementParEquipeById(string id){
        var classementParEquipe = _context._classement_general_view
            .Where(x => x.IdEquipe == id)
            .GroupBy(x => new { x.Etape, x.Equipe })
            .Select(g => new Classement_general_view{
                Etape = g.Key.Etape,
                Equipe = g.Key.Equipe,
                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }

    public List<Classement_general_view> GetPointsParEtape(){
        var classementParEquipe = _context._classement_general_view
            .GroupBy(x => new { x.Etape, x.Equipe })
            .Select(g => new Classement_general_view{
                Etape = g.Key.Etape,
                Equipe = g.Key.Equipe,
                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }

    public List<Classement_general_view> GetPointsParEtapeCoureur(string idEtape){
        var classementParEquipe = _context._classement_general_view
            .Where(x => x.IdEtape == idEtape)
            .GroupBy(x => new {
                 x.Etape, x.Equipe, x.Coureur , x.Genre , x.HeureDepart , x.HeureArrive ,  x.TempsPenalite , x.DureeTotale
                 })
            .Select(g => new Classement_general_view{
                Etape = g.Key.Etape,
                Equipe = g.Key.Equipe,
                Coureur = g.Key.Coureur,

                Point = g.Sum(x => x.Point)
            })
            .OrderByDescending(x => x.Point)
            .ToList();
        return classementParEquipe;
    }
        public List<Classement_general_view> FindAllByIdEtape(string id_etape){
        return _context._classement_general_view?
                .Where(i => i.IdEtape == id_etape)
                .ToList()?? new List<Classement_general_view>();
        }


}