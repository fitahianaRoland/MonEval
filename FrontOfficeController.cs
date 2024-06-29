// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Evaluation.Models;
// using X.PagedList;
// using Microsoft.EntityFrameworkCore;

// namespace Evaluation.Controllers;
// [ServiceFilter(typeof(SessionVerificationFilter))]
// public class FrontOfficeController : Controller
// {

//     private readonly ApplicationDbContext _context;
//     private readonly TypeMaisonRepository _type_maison;
//     private readonly ClientRepository _client;
//     private readonly DevisRepository _devis;
//     private readonly TypeFinitionRepository _type_finition;
//     private readonly HistoriqueDevisTravauxRepository _historique_travaux;
//     private readonly HistoriqueDevisFinitionRepository _historique_finition;
//     private readonly ILogger<FrontOfficeController> _logger;
//     private readonly PayementRepository _payement;

//     public FrontOfficeController(ILogger<FrontOfficeController> logger,ApplicationDbContext ap,TypeMaisonRepository typeM,TypeFinitionRepository typeF,DevisRepository d,ClientRepository c,HistoriqueDevisTravauxRepository ht,HistoriqueDevisFinitionRepository hf,PayementRepository p)
//     {
//         _logger = logger;
//         _context = ap;
//         _type_maison = typeM;
//         _type_finition = typeF;
//         _devis = d;
//         _client = c;
//         _historique_travaux = ht;
//         _historique_finition = hf;
//         _payement
//          = p;
//     }
//     public IActionResult Index()
//     {
//         return View();
//     }
//     public IActionResult Devis(int? page)
//     {
//         ViewBag.TypeMaison = _type_maison.FindAll();
//         ViewBag.TypeFinition = _type_finition.FindAll();
//         int pageSize = 10; // Nombre d'éléments par page
//         int pageNumber = (page ?? 1);
//         #pragma warning disable CS8604 // Possible null reference argument.
//             string? id_client = _client?.FindIdByContact(HttpContext.Session.GetString("Client"));
// #pragma warning restore CS8604 // Possible null reference argument. 
// #pragma warning disable CS8604 // Possible null reference argument.
//         ViewBag.Devis = _devis.FindAllByIdClient(id_client).ToPagedList(pageNumber, pageSize);
// #pragma warning restore CS8604 // Possible null reference argument.
//         return View();
//     }
//     public IActionResult CreeDevis(int? page)
//     {
//         try
//         {
//             int pageSize = 4; // Nombre d'éléments par page
//             int pageNumber = (page ?? 1); 
//             ViewBag.TypeMaison = _type_maison.FindAll().ToPagedList(pageNumber, pageSize);
//             ViewBag.TypeFinition = _type_finition.FindAll();
//         }
//         catch (Exception ex)
//         {
//             // Gérez les erreurs, par exemple en affichant un message d'erreur
//             string message = ex.Message;
//             return View("Exception",message);  
//         }    
//         return View();
//     }
//     public IActionResult AjouteDevis(string type_maison,string type_finition,DateTime date_debut_travaux)
//     {
//         using (var transaction = _context.Database.BeginTransaction())
//         {
//             try
//             {
                
//                 #pragma warning disable CS8604 // Possible null reference argument.
//                     string? id_client = _client?.FindIdByContact(HttpContext.Session.GetString("Client"));
//                 #pragma warning restore CS8604 // Possible null reference argument.
//                 var data_devis = new Devis{
//                     IdClient = id_client,
//                     IdTypeMaison = type_maison,
//                     IdTypeFinition = type_finition,
//                     DateCreation = DateTime.UtcNow,
//                     DateDebutTravaux = date_debut_travaux.ToUniversalTime(),
//                     MontantTotal = 0.0
//                 };
//                 string? insert_and_id_devis = _devis.AddDevis(data_devis);
//                 string sql1 = @$"
//                         INSERT INTO historique_devis_travaux (id_devis, id_travaux, prix_unitaire, quantite,id_unite)
//                         SELECT vdms.id_devis, vdms.id_travaux, vdms.prix_unitaire, vdms.quantite, vdms.id_unite
//                         FROM view_devis_maison_travaux AS vdms 
//                         WHERE vdms.id_devis = '{insert_and_id_devis}'";
//                 string sql2 = @$"
//                         INSERT INTO historique_devis_finition (id_devis, id_finition, pourcentage)
//                         SELECT d.id, tf.id, tf.pourcentage
//                         FROM devis d
//                         JOIN type_finition tf ON d.id_type_finition = tf.id WHERE d.id = '{insert_and_id_devis}'";
//                 _context.Database.ExecuteSqlRaw(sql1);
//                 _context.Database.ExecuteSqlRaw(sql2);
//                 string sqlUpdate = @$"
//                         UPDATE devis d
//                         SET montant_total = (SELECT m.montant_total FROM montant_total_devis m WHERE id = '{insert_and_id_devis}')
//                         WHERE d.id = '{insert_and_id_devis}' ";
//                 Console.WriteLine(sql1);
//                 Console.WriteLine(sql2);
//                 Console.WriteLine(sqlUpdate);
//                 Console.WriteLine("id devis io:"+insert_and_id_devis);
//                 _context.Database.ExecuteSqlRaw(sqlUpdate);
//                 transaction.Commit();

//             }
//             catch (Exception ex)
//             {
//                 transaction.Rollback();

//                 // Gérez les erreurs, par exemple en affichant un message d'erreur
//                 string message = ex.Message;
//                 return View("Exception",message);  
//             }  
//         }      
//             return RedirectToAction("Devis","FrontOffice");
//     }
   
//     public IActionResult Payement(string id_devis)
//     {
//         ViewBag.Devis = id_devis;
//         return View();
//     }
//     public IActionResult AjoutePayement(double montant,DateTime date_payement,string id_devis)
//     {
//         try
//         {
            
//             #pragma warning disable CS8604 // Possible null reference argument.
//                 string? id_client = _client?.FindIdByContact(HttpContext.Session.GetString("Client"));
//             #pragma warning restore CS8604 
//             var payement = new Payement{
//                 IdDevis = id_devis,
//                 IdClient = id_client,
//                 Montant = montant,
//                 DatePayement = date_payement.ToUniversalTime()
//             };
//             _payement.Add(payement);
//             Console.WriteLine($"montant:{montant} / date payement:{date_payement} / devis :{id_devis}");
//             return RedirectToAction("Devis","FrontOffice");
//         }
//         catch (Exception ex)
//             {
//                 // Gérez les erreurs, par exemple en affichant un message d'erreur
//                 string message = ex.Message;
//                 return View("Exception",message);  
//             }          
//     }
//     public IActionResult GeneratePdf(string id_devis)
//     {
//         byte[] pdfBytes =PdfGenerator.GeneratePdfOrderForm(id_devis,_historique_travaux);        
//         Console.WriteLine(id_devis);
//         // Renvoyer le PDF généré comme un fichier téléchargeable
//         return File(pdfBytes, "application/pdf", $"{DateTime.Now}.pdf");
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }
