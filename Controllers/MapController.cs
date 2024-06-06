using dotnetEval.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace dotnetEval;

public class MapController : Controller
{
    private readonly AppDbContext _context;
    public MapController(AppDbContext context){
        _context = context;
    }
    public IActionResult MapPage(){
        List<Map_data> mapDataList = _context._map_data.ToList();
        ViewBag.list_map_data = JsonConvert.SerializeObject(mapDataList);
        return View();
    }
}
