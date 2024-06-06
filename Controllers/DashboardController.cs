// using dotnetEval.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace dotnetEval;

// public class DashboardController : Controller
// {
//     private readonly TypeFinitionRepository _typeFinitionRepository;
//     private readonly DevisRepository _devisRepository;
//     public DashboardController(DevisRepository devisRepository){
//         _devisRepository = devisRepository;
//     }
//     public IActionResult DashboardPage(){
//         ViewBag.TotalDevis = _devisRepository.GetTotalMontant();
//         Console.WriteLine("montant : " + _devisRepository.GetTotalMontant());
//         return View();
//     }

// }
