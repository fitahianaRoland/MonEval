// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using dotnetEval.Models;
// using dotnetEval.repository;

// namespace dotnetEval.service
// {
//     public class AnnonceSerice
//     {
//         private readonly AnnonceRepository _annonceRepository;

//         public AnnonceSerice(AnnonceRepository annonceRepository)
//         {
//             _annonceRepository = annonceRepository;
//         }

//         public void InsertionBaseAnnonce(string idMembreLogin,string imageName, string name, string libelle, double prix)
//         {
//             _annonceRepository.InsertionBaseAnnonce(idMembreLogin,imageName, name, libelle,prix);
//         }

//         public List<Annonce> GetAllAnnonce()
//         {
//             return _annonceRepository.GetAllAnnonce();
//         }
//     }
// }