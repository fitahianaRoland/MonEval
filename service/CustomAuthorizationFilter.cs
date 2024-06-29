using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string IdEquipe  = context.HttpContext.Session.GetString("id_equipe");
        string idAdmin  = context.HttpContext.Session.GetString("id_admin");
        bool equipeBool = string.IsNullOrEmpty(IdEquipe);
        bool adminBool = string.IsNullOrEmpty(idAdmin);
        
        if (equipeBool == true && adminBool == true)
        {
            // Si la session n'existe pas, rediriger vers la page de connexion
            context.Result = new RedirectToActionResult("LoginPageEquipe", "Login", null);
        }
        else if (equipeBool == false || adminBool == true)
        {
            // Console.WriteLine(statues);
            // Si la session n'existe pas, rediriger vers la page de connexion
            context.Result = new RedirectToActionResult("AuthentificationNotifPage", "Authentification", null);
        }
    }
}
