using dotnetEval.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetEval;

public class CategorieController :Controller
{
    private readonly ILogger<CategorieController> _logger;
    private readonly AppDbContext _context;
    public CategorieController(ILogger<CategorieController> logger , AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    public IActionResult GenererCategoriePage()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
        ViewBag.SuccesMessage = TempData["SuccesMessage"] as string;
        return View();
    }

    public IActionResult GenererCategorieBase()
    {
        try
        {
            _context.Database.ExecuteSqlRaw("select f_coureur_categorie() ");
            Console.WriteLine("generer");
            TempData["SuccesMessage"] = "genereration success";
            return Redirect("GenererCategoriePage");
        }
        catch (Exception ex){
            TempData["ErrorMessage"] = ex.Message;
            return Redirect("GenererCategoriePage");
        }
    }

}
