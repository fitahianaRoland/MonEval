using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class TempEtapeRepository
{
    private readonly AppDbContext _context;

    public TempEtapeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<TempEtape> FindAll()
    {
        return _context._temp_etape?.ToList()?? new List<TempEtape>();
    }

    public void Add(TempEtape temp_etape)
    {
        if (temp_etape == null)
        {
            throw new ArgumentNullException(nameof(temp_etape));
        }

        _context._temp_etape.Add(temp_etape);
        _context.SaveChanges();
    }

    public void Update(TempEtape  temp_etape)
    {
        if (temp_etape == null)
        {
            throw new ArgumentNullException(nameof(temp_etape));
        }

        _context._temp_etape.Update(temp_etape);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var temp_etapeToDelete = _context._temp_etape.Find();
        if (temp_etapeToDelete != null)
        {
            _context._temp_etape.Remove(temp_etapeToDelete);
            _context.SaveChanges();
        }
    }
}