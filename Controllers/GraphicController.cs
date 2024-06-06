using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace dotnetEval;

public class GraphicController : Controller
{
    public IActionResult ChartJsPage(){
        return View();
    }
}
