using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetEval.Data;
using dotnetEval.Models;
using dotnetEval.service;

public class AdministrateurRepository
{
    private readonly AppDbContext _context;

    public AdministrateurRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Administrateur> FindAll()
    {
        return _context._administrateur?.ToList()?? new List<Administrateur>();
    }

    public void Add(Administrateur administrateur)
    {
        if (administrateur == null)
        {
            throw new ArgumentNullException(nameof(administrateur));
        }

        _context._administrateur.Add(administrateur);
        _context.SaveChanges();
    }

    public void Update(Administrateur  administrateur)
    {
        if (administrateur == null)
        {
            throw new ArgumentNullException(nameof(administrateur));
        }

        _context._administrateur.Update(administrateur);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var administrateurToDelete = _context._administrateur.Find(id);
        if (administrateurToDelete != null)
        {
            _context._administrateur.Remove(administrateurToDelete);
            _context.SaveChanges();
        }
    }

    public string  VerificationLoginAdmin(string email, string password)
    {
        var membre = _context._administrateur.FirstOrDefault(m => m.Email == email && m.MotDePasse == password);
        if (membre == null) {throw new ArgumentException("verifier votre email ou mot de passe !!");}
        return membre.Id; 
    }
}