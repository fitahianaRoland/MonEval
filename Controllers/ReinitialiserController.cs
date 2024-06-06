using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetEval.Models;
using dotnetEval.Data;

namespace dotnetEval.Controllers;

public class Reinitialiser : Controller
{
    private readonly ILogger<Reinitialiser> _logger;
    private readonly Helper _helper;
    private readonly AppDbContext _context;

    public Reinitialiser(ILogger<Reinitialiser> logger,Helper helper,AppDbContext context)
    {
        _logger = logger;
        _helper = helper;
        _context = context;
    }

    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public IActionResult ReinitialiserBasePage(){
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }


    public IActionResult ReinitialiserBase()
    {
        try{
            _helper.ResetDataBase(_context);
            ViewBag.SuccesMessage = "delete table";
            Console.WriteLine("delete table");
            Redirect("ReinitialiserBase");
        }
        catch (System.Exception){
            ViewBag.ErrorMessage = "error delete table" ;
            Redirect("ReinitialiserBase");
        }
        return View("ReinitialiserBasePage");   
    }

}
