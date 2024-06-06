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
    public class EtapeController : Controller
    {

        private readonly EtapeRepository _etapeRepository;
        private readonly GetPaginationModelService _getPaginationModelService;
        private readonly ILogger<EtapeController> _logger;
        private readonly EtapeCoureurRepository _etapeCoureurRepository;
        private readonly VEtapeCoureurEquipeRepository _vEtapeCoureurEquipeRepository;
        private readonly VEtapeCoureurEquipeDureeRepository _vEtapeCoureurEquipeDureeRepository;


        public EtapeController(EtapeRepository etapeRepository, 
        GetPaginationModelService getPaginationModelService,
         EtapeCoureurRepository etapeCoureurRepository,
         ILogger<EtapeController> logger,
         VEtapeCoureurEquipeDureeRepository vetapeCoureurEquipeDuree,
         VEtapeCoureurEquipeRepository vEtapeCoureurEquipeRepository
         )
        {
            _vEtapeCoureurEquipeRepository = vEtapeCoureurEquipeRepository;
            _etapeRepository = etapeRepository;
            _getPaginationModelService = getPaginationModelService;
            _etapeCoureurRepository = etapeCoureurRepository;
            _logger = logger;
            _vEtapeCoureurEquipeDureeRepository = vetapeCoureurEquipeDuree;
        }

        public IActionResult ListEtapePageEquipe(int page = 1)
        {
            var model = _getPaginationModelService.ListShow(page, _etapeRepository.FindAllOrderBy().ToList());
            return View("ListEtapePageEquipe", model);
        }

        public IActionResult InterfaceEquipPage(int page = 1)
        {
            string? idEquipe = HttpContext.Session.GetString("id_equipe");
            ViewBag.listEtapes =  _vEtapeCoureurEquipeDureeRepository.FindAllByIdEquipe(idEquipe);
            return View("interfaceEquipPage");
        }


        [HttpPost]
        public JsonResult EtapeAffectationEquipe([FromQuery] string[] id_coureur)
        {
            try
            {
                _logger.LogInformation("EtapeAffectationEquipe called");
                string id_etape = HttpContext.Session.GetString("id_etape");
                string id_equipe = HttpContext.Session.GetString("id_equipe");
                Console.WriteLine("id etape : " + id_etape);
                // nombre coureur dans etapes
                double? coureurDansEtapes = _etapeRepository.GetNombreCoureurDansEtape(id_etape);


                double nombreEquipeinserer = _vEtapeCoureurEquipeRepository.GetNombreEtapeEquipe(id_equipe,id_etape);
                Console.WriteLine("nb equipe selectionner : " + nombreEquipeinserer);
                Console.WriteLine("nb equipe inserre : " + nombreEquipeinserer);
                if (string.IsNullOrEmpty(id_etape)){
                    _logger.LogWarning("id_etape is null or empty");
                    return Json(new { success = false, message = "Session expired or invalid step ID." });
                }
                if (nombreEquipeinserer >= coureurDansEtapes){
                    _logger.LogWarning("Number of runners exceeds the allowed limit");
                    return Json(new { success = false, message = "l'etape est deja complet" });
                }
                if (id_coureur.Length > coureurDansEtapes){
                    _logger.LogWarning("Number of runners exceeds the allowed limit");
                    return Json(new { success = false, message = "Nombre de coureurs supérieur à la limite autorisée." });
                }
                Console.WriteLine("" + coureurDansEtapes);
                _etapeCoureurRepository.AddAllCoureur(id_etape, id_coureur);
                _logger.LogInformation("Affectation réussie");
                return Json(new { success = true, message = "AFFECTATION RÉUSSIE!" });
            }
            catch (Exception ex){
                _logger.LogError(ex, "Error in EtapeAffectationEquipe");
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }



        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult ListEtapePageAdmin(int page = 1)
        {
            ViewBag.listEtapes = _etapeRepository.FindAll();
            return View("ListEtapePageAdmin");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}