using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class EtapeRepository
{
    private readonly AppDbContext _context;

    public EtapeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Etape> FindAll()
    {
        return _context._etape?.ToList()?? new List<Etape>();
    }
    public List<Etape> FindAllOrderBy(){
        return _context._etape
                    .OrderBy(e => e.RangEtape)
                    .ToList() ?? new List<Etape>();
    }

    public void Add(Etape etape)
    {
        if (etape == null)
        {
            throw new ArgumentNullException(nameof(etape));
        }

        _context._etape.Add(etape);
        _context.SaveChanges();
    }

    public void Update(Etape  etape)
    {
        if (etape == null)
        {
            throw new ArgumentNullException(nameof(etape));
        }

        _context._etape.Update(etape);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var etapeToDelete = _context._etape.Find(id);
        if (etapeToDelete != null)
        {
            _context._etape.Remove(etapeToDelete);
            _context.SaveChanges();
        }
    }

public double? GetNombreCoureurDansEtape(string? id_etape)
{
    Etape etape = _context._etape.SingleOrDefault(c => c.Id == id_etape);
    return etape != null ? etape.NombreCoureurEquipe : 0;
}
}