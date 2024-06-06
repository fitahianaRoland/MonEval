using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotnetEval.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestPDF;

namespace dotnetEval.Controllers
{
    public class PdfController : Controller
    {
        // private readonly ILogger<PdfController> _logger;
        private readonly PdfService _pdfService;

        private readonly ClassementGeneralViewRepository _classementGeneralViewRepository;
        

        public PdfController(PdfService pdfService , ClassementGeneralViewRepository classementGeneralViewRepository)
        {
            _pdfService = pdfService;
            _classementGeneralViewRepository = classementGeneralViewRepository;
        }

        public IActionResult GeneratePdf()
        {
            // try
            // {
                string directoryPath = @"D:\Fianarana\s6\dotnetEval\pdf";
                string fileName = $"generated_pdf_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = Path.Combine(directoryPath, fileName);

                _pdfService.GenerationPdf(filePath, _classementGeneralViewRepository.GetClassementGeneral());
                // Console.WriteLine(" nom equipe " +  _classementGeneralViewRepository.GetClassementGeneral()[0]);
                return Ok($"PDF généré et enregistré à l'emplacement : {filePath}");
            // }
            // catch (Exception ex)
            // {
            //     return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            // }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){  return View("Error!");}
    }
}