using dotnetEval.Data;
using dotnetEval.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace dotnetEval;

public class ClassementController : Controller
{
    private readonly ClassementGeneralViewRepository _classementGeneralViewRepository;

    private readonly EtapeRepository _etapeRepository;

    private readonly ClassementGeneralCategorieViewRepository _classementGeneralCategorieViewRepository;
    public readonly CategorieRepository _categorieRepository;
    private readonly GetPaginationModelService _getPaginationModelService;
    private readonly ClassementGeneralCoefficientEtapeRangRepository _classementGeneralCoefficientEtapeRangRepository;
    private readonly ClassementGeneralCoefficientEtapeRepository _classementGeneralCoefficientEtapeRepository;
    public ClassementController(ClassementGeneralViewRepository classementGeneralViewRepository,  
    GetPaginationModelService getPaginationModelService ,
    CategorieRepository categorieRepository,
    ClassementGeneralCategorieViewRepository classementGeneralCategorieViewRepository,
    EtapeRepository etapeRepository,
    ClassementGeneralCoefficientEtapeRangRepository classementGeneralCoefficientEtapeRangRepository,
    ClassementGeneralCoefficientEtapeRepository classementGeneralCoefficientEtapeRepository
    ){
        _classementGeneralViewRepository = classementGeneralViewRepository;
        _getPaginationModelService = getPaginationModelService;
        _categorieRepository = categorieRepository;
        _classementGeneralCategorieViewRepository = classementGeneralCategorieViewRepository;
        _etapeRepository = etapeRepository;
        _classementGeneralCoefficientEtapeRangRepository = classementGeneralCoefficientEtapeRangRepository;
        _classementGeneralCoefficientEtapeRepository = classementGeneralCoefficientEtapeRepository;
    }
    public IActionResult ClassementGeneralePage(int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralViewRepository.GetClassementGeneral());
        return View("ClassementGeneralePage", model);
    }

    public IActionResult ClassementParEquipePage(int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralViewRepository.GetClassementParEquipe());
        return View("ClassementParEquipePage", model);
    }

    public IActionResult ClassementParEtapePage(int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralViewRepository.GetPointsParEtape());
        return View("ClassementParEtapePage", model);
    }
    public IActionResult ClassementParCategoriePage(int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralViewRepository.GetClassementParEquipe());
        ViewBag.listCategorie =  _categorieRepository.FindAll();
        return View("ClassementParCategoriePage", model);
    }

    public IActionResult ClassementParCategoriePageById(string id,int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralViewRepository.GetClassementParEquipeById(id));
        ViewBag.listCategorie =  _categorieRepository.FindAll();
        return View("ClassementParCategoriePageById", model);
    }
    public IActionResult ClassementParCategorieResultPage(){
        return View();
    }
    public IActionResult ClassementParCategorieResultBase(string idCategorie , int page = 1){
        var model = _getPaginationModelService.ListShow(page, _classementGeneralCategorieViewRepository.GetClassementParCategorie(idCategorie));
        ViewBag.listCategorie =  _categorieRepository.FindAll();
        return View("ClassementParCategorieResultPage", model);
    }
    
    // ----------------------------- etape list
    
    public IActionResult ListEtapePage(){
        ViewBag.listEtape = _classementGeneralCoefficientEtapeRepository.GetPointsParEtape();
        return View();
    }
    
    public IActionResult ClassementParEtapeCoureur(string id){
        Console.WriteLine(" idEtape :" + id);
        ViewBag.listEtapeCoureur = _classementGeneralCoefficientEtapeRangRepository.FindAllByIdEtape(id);
        // ViewBag.maxPoints = _classementGeneralViewRepository.FindAllByIdEtape(id).Max(liste => liste.Point);
        return View("ClassementParEtapeCoureur");
    }




    
}
