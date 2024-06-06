using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class EtapeEquipePenaliteRepository
{
    private readonly AppDbContext _context;

    public EtapeEquipePenaliteRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<EtapeEquipePenalite> FindAll()
    {
        return _context._etape_equipe_penalite?.ToList()?? new List<EtapeEquipePenalite>();
    }

    public void Add(EtapeEquipePenalite etape_equipe_penalite)
    {
        if (etape_equipe_penalite == null)
        {
            throw new ArgumentNullException(nameof(etape_equipe_penalite));
        }

        _context._etape_equipe_penalite.Add(etape_equipe_penalite);
        _context.SaveChanges();
    }

    public void Update(EtapeEquipePenalite  etape_equipe_penalite)
    {
        if (etape_equipe_penalite == null)
        {
            throw new ArgumentNullException(nameof(etape_equipe_penalite));
        }

        _context._etape_equipe_penalite.Update(etape_equipe_penalite);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var etape_equipe_penaliteToDelete = _context._etape_equipe_penalite.Find(id);
        if (etape_equipe_penaliteToDelete != null)
        {
            _context._etape_equipe_penalite.Remove(etape_equipe_penaliteToDelete);
            _context.SaveChanges();
        }
    }
}