using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class TempResultatRepository
{
    private readonly AppDbContext _context;

    public TempResultatRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<TempResultat> FindAll()
    {
        return _context._temp_resultat?.ToList()?? new List<TempResultat>();
    }

    public void Add(TempResultat temp_resultat)
    {
        if (temp_resultat == null)
        {
            throw new ArgumentNullException(nameof(temp_resultat));
        }

        _context._temp_resultat.Add(temp_resultat);
        _context.SaveChanges();
    }

    public void Update(TempResultat  temp_resultat)
    {
        if (temp_resultat == null)
        {
            throw new ArgumentNullException(nameof(temp_resultat));
        }

        _context._temp_resultat.Update(temp_resultat);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var temp_resultatToDelete = _context._temp_resultat.Find();
        if (temp_resultatToDelete != null)
        {
            _context._temp_resultat.Remove(temp_resultatToDelete);
            _context.SaveChanges();
        }
    }
}