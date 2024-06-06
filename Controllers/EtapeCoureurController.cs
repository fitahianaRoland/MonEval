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
    public class EtapeCoureurController : Controller
    {

        private readonly EtapeCoureurRepository _etapeCoureurRepository;
        private readonly TempsCoureurRepository _tempsCoureurRepository;
        private readonly GetPaginationModelService _getPaginationModelService;
        private readonly ILogger<EtapeCoureurController> _logger;


        public EtapeCoureurController(EtapeCoureurRepository etapeCoureurRepository, GetPaginationModelService getPaginationModelService,ILogger<EtapeCoureurController> logger , TempsCoureurRepository tempsCoureurRepository)
        {
            _etapeCoureurRepository = etapeCoureurRepository;
            _getPaginationModelService = getPaginationModelService;
            _logger = logger;
            _tempsCoureurRepository = tempsCoureurRepository;
        }


        public IActionResult ListEtapeCoureurPageAdmin(string id ,int page = 1 ){
            HttpContext.Session.SetString("id_etape", id);
            ViewBag.listCoureurNotInTempsCoureur = _etapeCoureurRepository.GetEtapeCoureurNotInTempsCoureur(id).ToList();
            ViewBag.listTempsCoureur = _tempsCoureurRepository.finById(id);
            return View("ListEtapeCoureurPage");
            // var model = _getPaginationModelService.ListShow(page, _etapeCoureurRepository.GetEtapeCoureurNotInTempsCoureur(id).ToList());
            // return View("ListEtapeCoureurPage", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}