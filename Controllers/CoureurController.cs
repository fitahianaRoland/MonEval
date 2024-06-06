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
    public class CoureurController : Controller
    {

        private readonly CoureurRepository _coureurRepository;
        private readonly EtapeCoureurRepository _etapeCoureurRepository;
        private readonly GetPaginationModelService _getPaginationModelService;


        public CoureurController(CoureurRepository coureurRepository, GetPaginationModelService getPaginationModelService, EtapeCoureurRepository etapeCoureurRepository)
        {
            _coureurRepository = coureurRepository;
            _getPaginationModelService = getPaginationModelService;
            _etapeCoureurRepository = etapeCoureurRepository;
        }

        public IActionResult CoureurListPageEquipe(string id_etape, int page = 1)
        {
            Console.WriteLine("idEtape"+ id_etape);
            HttpContext.Session.SetString("id_etape", id_etape);
            string id_equipe = HttpContext.Session.GetString("id_equipe");
            var model = _getPaginationModelService.ListShow(page, _coureurRepository.GetCoureurNotInEtapes(id_equipe,id_etape));
            return View("CoureurListPageEquipe", model);
        }

        public IActionResult CoureurListPageAdmin(string id, int page = 1)
        {
            HttpContext.Session.SetString("id_etape", id);
            string id_equipe = HttpContext.Session.GetString("id_equipe");
            var coureurs = _etapeCoureurRepository.GetCoureursByEtape(id);
            var model = _getPaginationModelService.ListShow(page, coureurs.Select(ec => ec.Coureur).ToList());
            return View("CoureurListPageAdmin", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}