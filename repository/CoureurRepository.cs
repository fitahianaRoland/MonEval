using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;
using Npgsql;

public class CoureurRepository
{
    private readonly AppDbContext _context;

    public CoureurRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Coureur> FindAll()
    {
        return _context._coureur?.ToList()?? new List<Coureur>();
    }

    public List<Coureur> FindAllByEquipe(string id_equipe)
    {
        return _context._coureur
                    .Where(c => c.IdEquipe == id_equipe)
                    .ToList() ?? new List<Coureur>();
    }
    public void Add(Coureur coureur)
    {
        if (coureur == null)
        {
            throw new ArgumentNullException(nameof(coureur));
        }

        _context._coureur.Add(coureur);
        _context.SaveChanges();
    }

    public void Update(Coureur  coureur)
    {
        if (coureur == null)
        {
            throw new ArgumentNullException(nameof(coureur));
        }

        _context._coureur.Update(coureur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var coureurToDelete = _context._coureur.Find(id);
        if (coureurToDelete != null)
        {
            _context._coureur.Remove(coureurToDelete);
            _context.SaveChanges();
        }
    }
    public List<Coureur> GetCoureurNotInEtapes(string id_equipe ,string etapeId)
    {
        string query = @"
            select * from coureur where id_equipe = '"+id_equipe+"' and id not in (select id_coureur from etape_coureur where id_etape = '"+etapeId+"')";

        var Coureurs = _context._coureur
                                    .FromSqlRaw<Coureur>(query, new NpgsqlParameter("@etapeId", etapeId))
                                    .ToList();

        foreach (var item in Coureurs)
        {
            Console.WriteLine(item.Id);
        }
        return Coureurs;
    }
}