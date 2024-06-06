using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class EquipeRepository
{
    private readonly AppDbContext _context;

    public EquipeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Equipe> FindAll()
    {
        return _context._equipe?.ToList()?? new List<Equipe>();
    }

    public void Add(Equipe equipe)
    {
        if (equipe == null)
        {
            throw new ArgumentNullException(nameof(equipe));
        }

        _context._equipe.Add(equipe);
        _context.SaveChanges();
    }

    public void Update(Equipe  equipe)
    {
        if (equipe == null)
        {
            throw new ArgumentNullException(nameof(equipe));
        }

        _context._equipe.Update(equipe);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var equipeToDelete = _context._equipe.Find(id);
        if (equipeToDelete != null)
        {
            _context._equipe.Remove(equipeToDelete);
            _context.SaveChanges();
        }
    }

    public string  VerificationLoginEquipe(string email, string password)
    {
        var membre = _context._equipe.FirstOrDefault(m => m.Login == email && m.MotDePasse == password);
        if (membre == null) {throw new ArgumentException("verifier votre email ou mot de passe !!");}
        return membre.Id; 
    }
}