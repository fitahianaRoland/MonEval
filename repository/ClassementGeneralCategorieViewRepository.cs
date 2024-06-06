using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class ClassementGeneralCategorieViewRepository
{
    private readonly AppDbContext _context;

    public ClassementGeneralCategorieViewRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<ClassementGeneralCategorieView> FindAll()
    {
        return _context._classement_general_categorie_view?.ToList()?? new List<ClassementGeneralCategorieView>();
    }

    public void Add(ClassementGeneralCategorieView classement_general_categorie_view)
    {
        if (classement_general_categorie_view == null)
        {
            throw new ArgumentNullException(nameof(classement_general_categorie_view));
        }

        _context._classement_general_categorie_view.Add(classement_general_categorie_view);
        _context.SaveChanges();
    }

    public void Update(ClassementGeneralCategorieView  classement_general_categorie_view)
    {
        if (classement_general_categorie_view == null)
        {
            throw new ArgumentNullException(nameof(classement_general_categorie_view));
        }

        _context._classement_general_categorie_view.Update(classement_general_categorie_view);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var classement_general_categorie_viewToDelete = _context._classement_general_categorie_view.Find();
        if (classement_general_categorie_viewToDelete != null)
        {
            _context._classement_general_categorie_view.Remove(classement_general_categorie_viewToDelete);
            _context.SaveChanges();
        }
    }

    public List<ClassementGeneralCategorieView> GetClassementParCategorie(string idCategorie){
    var classementGeneralCategorie = _context._classement_general_categorie_view
        .Where(x => x.IdCategorie == idCategorie)
        .GroupBy(x => new { x.NomCategorie, x.Equipe })
        .Select(g => new ClassementGeneralCategorieView {
            NomCategorie = g.Key.NomCategorie,
            Equipe = g.Key.Equipe,
            Point = g.Sum(x => x.Point)
        })
        .OrderByDescending(x => x.Point)
        .ToList();

    return classementGeneralCategorie;
    }
}