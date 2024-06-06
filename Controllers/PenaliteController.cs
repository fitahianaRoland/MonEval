using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetEval.Models;
using dotnetEval.Data;
using dotnetEval.service;

namespace dotnetEval.Controllers;

public class PenaliteController : Controller
{
    private readonly ILogger<PenaliteController> _logger;
    private readonly AppDbContext _context;
    private readonly EtapeRepository _etapeRepository;
    private readonly EquipeRepository _equipeRepository;
    private readonly EtapeEquipePenaliteRepository _etapeEquipePenaliteRepository;

    public PenaliteController(ILogger<PenaliteController> logger,
    AppDbContext context,
    EtapeEquipePenaliteRepository etapeEquipePenaliteRepository,
    EtapeRepository etapeRepository,
    EquipeRepository equipeRepository
    )
    {
        _logger = logger;
        _context = context;
        _etapeEquipePenaliteRepository = etapeEquipePenaliteRepository;
        _etapeRepository = etapeRepository;
        _equipeRepository = equipeRepository;
    }

    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public IActionResult ListPenalitePage(){
        ViewBag.listPenalite = _etapeEquipePenaliteRepository.FindAll();
        return View();
    }
    public IActionResult AddPenalitePage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.listEtapes = _etapeRepository.FindAll();
        ViewBag.listEquipes =  _equipeRepository.FindAll();
        return View();
    }
    public IActionResult AddPenaliteBase(string idEtape,string idEquipe,TimeSpan time){
        try{
            EtapeEquipePenalite penalite = new EtapeEquipePenalite(idEtape, idEquipe, time);
            _etapeEquipePenaliteRepository.Add(penalite);
            return Redirect("ListPenalitePage");
        }
        catch (Exception ex){
            ViewBag.ErrorMessage = "error to insert"+ex.Message ;
            return View("AddPenalitePage");
        }
    }
    public IActionResult DeletePenaliteBase(string id){
        try{
            _etapeEquipePenaliteRepository.Delete(id);
            return Redirect("../ListPenalitePage");
        }
        catch (Exception ex){
            ViewBag.ErrorMessage = "error to insert"+ex.Message ;
            return View("AddPenalitePage");
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }
}
