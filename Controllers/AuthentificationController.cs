using Microsoft.AspNetCore.Mvc;

namespace dotnetEval;

public class AuthentificationController : Controller
{
    public IActionResult AuthentificationNotifPage(){
        return View();
    }
}
