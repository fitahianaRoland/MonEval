using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetEval.Models;
using dotnetEval.service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace dotnetEval.Controllers;

public class LoginController : Controller
{
    private readonly EquipeRepository _equipeRepository;
    public readonly AdministrateurRepository _administrateurRepository;
    public LoginController(EquipeRepository equipeRepository,AdministrateurRepository administrateurRepository){
        _equipeRepository = equipeRepository;
        _administrateurRepository = administrateurRepository;
    }
    public IActionResult LoginPage()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        return View();
    }
    public IActionResult LoginPageEquipe()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        return View();
    }
    public IActionResult LoginPageAdmin()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        return View();
    }
    
    public IActionResult RegisterPage()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        return View();
    }
    public IActionResult VerificationLoginAdmin(string email, string password)
    {
        try
        {
            string id = _administrateurRepository.VerificationLoginAdmin(email, password);
            Console.WriteLine("verification succes");

            HttpContext.Session.SetString("id_admin", id);
            return Redirect("../Etape/InterfaceEquipPage");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("verification faild");
            TempData["ErrorMessage"] = ex.Message;
            return Redirect("LoginPageAdmin");
        }
    }
    public IActionResult VerificationLoginEquipe(string email, string password)
    {
        try
        {
            string id = _equipeRepository.VerificationLoginEquipe(email, password);
            HttpContext.Session.SetString("id_equipe", id);
            return Redirect("../Etape/InterfaceEquipPage");
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return Redirect("LoginPageEquipe");
        }
    }
    


    // public IActionResult RegisterBase(string name,string lastName,string email,DateTime dateNaissance,string genre,string Password,string passwordConfirm)
    // {
    //     try
    //     {
    //         _membresService.RegisterBase(name, lastName, email,dateNaissance,genre,Password,passwordConfirm);
    //         return View("LoginPage");
    //     }
    //     catch (ArgumentException ex){
    //         TempData["ErrorMessage"] = ex.Message;
    //         return Redirect("RegisterPage");
    //     }
    // }


    public IActionResult Deconnexion(){
        // Supprimer toutes les sessions
        HttpContext.Session.Clear();
        // Déconnecter l'utilisateur en supprimant le cookie d'authentification
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Console.WriteLine("deconnexion");
        return Redirect("../Login/LoginPageEquipe");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
