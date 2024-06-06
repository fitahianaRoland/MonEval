using System.Text;
using ClosedXML.Excel;
using dotnetEval.Data;
using dotnetEval.Models;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using dotnetEval.service;

namespace dotnetEval;

[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class ImportController : Controller
{
    private readonly ILogger<ImportController> _logger;
    private readonly AppDbContext _context;
    private readonly InsertionTableRationnel _insertionTableRationnel;
    private readonly Import _csv;

//     #pragma warning disable CS8618
    public ImportController(ILogger<ImportController> logger, AppDbContext context, Import import, InsertionTableRationnel insertionTableRationnel)
   {
        _context = context;
        _logger = logger;
        _csv = import;
        _insertionTableRationnel = insertionTableRationnel;
    }
    public IActionResult ImportPointsPage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }
    public IActionResult ImportEtapePage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }
    public IActionResult ImportResultatPage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }
    public IActionResult ImportDonnesPage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }
    public IActionResult ImportDonnesBase(IFormFile etapesFile,IFormFile resultFile)
    {
        if (etapesFile == null || etapesFile.Length == 0 || resultFile == null || resultFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Le fichier est vide ou n'existe pas.";
                return Redirect("ImportDonnesPage");
            }
            try
            {
                _csv.ImportCsvToDatabase("temp_etape",etapesFile, TempEtape.MapTempEtape);
                _csv.ImportCsvToDatabase("temp_resultat",resultFile, TempResultat.MapTempResultat);
                _insertionTableRationnel.InsertDataFromTempEtapes();
                _insertionTableRationnel.InsertDataFromTempResult();
                TempData["SuccesMessage"] = "import succes";
                return Redirect("ImportDonnesPage");
            }
         
        catch (Exception ex)
        {
            // Gérez les erreurs, par exemple en affichant un message d'erreur
            TempData["ErrorMessage"] = ex.Message;
            return Redirect("ImportDonnesPage");
        }          
    }
    public IActionResult ImportPointsBase(IFormFile file)
    {
        if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Le fichier est vide ou n'existe pas.";
                return Redirect("ImportPointsPage");
            }
            try
            {
                _csv.ImportCsvToDatabase("temp_points",file, TempPoints.MapTempPoint);
                _insertionTableRationnel.InsertDataFromTempPoints();
                TempData["SuccesMessage"] = "import succes";
                return Redirect("ImportPointsPage");
            }
         
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return Redirect("ImportPointsPage");
        }          
    }
}
