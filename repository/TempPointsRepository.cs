using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class TempPointsRepository
{
    private readonly AppDbContext _context;

    public TempPointsRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<TempPoints> FindAll()
    {
        return _context._temp_points?.ToList()?? new List<TempPoints>();
    }

    public void Add(TempPoints temp_points)
    {
        if (temp_points == null)
        {
            throw new ArgumentNullException(nameof(temp_points));
        }

        _context._temp_points.Add(temp_points);
        _context.SaveChanges();
    }

    public void Update(TempPoints  temp_points)
    {
        if (temp_points == null)
        {
            throw new ArgumentNullException(nameof(temp_points));
        }

        _context._temp_points.Update(temp_points);
        _context.SaveChanges();
    }

    public void Delete( )
    {
        var temp_pointsToDelete = _context._temp_points.Find();
        if (temp_pointsToDelete != null)
        {
            _context._temp_points.Remove(temp_pointsToDelete);
            _context.SaveChanges();
        }
    }
}