using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using dotnetEval.service;
using dotnetEval.Models;

namespace dotnetEval.Controllers
{
    // [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public class TempsCoureur : Controller
    {

        private readonly TempsCoureurRepository _tempsCoureurRepository;
        private readonly ExeptionMessage _exeptionMessage;
        public TempsCoureur(TempsCoureurRepository tempsCoureurRepository, ExeptionMessage exeptionMessage)
        {
            _tempsCoureurRepository = tempsCoureurRepository;
            _exeptionMessage = exeptionMessage;
        }

        public IActionResult AjoutTempsCoureur(string id){
            HttpContext.Session.SetString("id_coureur", id);
            return Redirect("../AjoutTempsCoureurPage");
        }

        public IActionResult AjoutTempsCoureurPage(){
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View();
        }

        public IActionResult InsertionTempsCoureur(DateTime heure_debut,DateTime heure_arrive)
        {
            try
            {
                _exeptionMessage.ConfirmationHeure(heure_debut, heure_arrive);
                Console.WriteLine("verification heure fait");
                string id_etape = HttpContext.Session.GetString("id_etape");
                string id_coureur = HttpContext.Session.GetString("id_coureur");
                _tempsCoureurRepository.InsertionTempsCoureur(id_etape,id_coureur,heure_debut,heure_arrive);
                Console.WriteLine("insertion fait");
                return Redirect("../Etape/ListEtapePageAdmin");
            }
            catch (ArgumentException ex){
                TempData["ErrorMessage"] = ex.Message;  
                return Redirect("AjoutTempsCoureurPage");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}