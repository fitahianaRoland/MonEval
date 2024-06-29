using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class CoefficientEtapeRepository
{
    private readonly AppDbContext _context;

    public CoefficientEtapeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<CoefficientEtape> FindAll()
    {
        return _context._coefficient_etape?.ToList()?? new List<CoefficientEtape>();
    }

    public void Add(CoefficientEtape coefficient_etape)
    {
        if (coefficient_etape == null)
        {
            throw new ArgumentNullException(nameof(coefficient_etape));
        }

        _context._coefficient_etape.Add(coefficient_etape);
        _context.SaveChanges();
    }

    public void Update(CoefficientEtape  coefficient_etape)
    {
        if (coefficient_etape == null)
        {
            throw new ArgumentNullException(nameof(coefficient_etape));
        }

        _context._coefficient_etape.Update(coefficient_etape);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var coefficient_etapeToDelete = _context._coefficient_etape.Find(id);
        if (coefficient_etapeToDelete != null)
        {
            _context._coefficient_etape.Remove(coefficient_etapeToDelete);
            _context.SaveChanges();
        }
    }
}