using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Linq.Models;

namespace Linq.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46, "Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
        Eruption ChileEruption = eruptions.FirstOrDefault(e=> e.Location == "Chile");
        ViewBag.ChileEruption=ChileEruption;
        var HawaiianEruption = eruptions.FirstOrDefault(e=> e.Location == "Hawaiian")? .Volcano??"Nuk Egziston";
        ViewBag.HawaiianEruption=HawaiianEruption;
        var GreenlandEruption= eruptions.FirstOrDefault(e=> e.Location== "Greenland")? .Volcano??"Nuk Egziston";
        ViewBag.GreenlandEruption=GreenlandEruption;
        Eruption NewZeland = eruptions.FirstOrDefault(e=> e.Location == "New Zealand");
        ViewBag.NewZelandEruption=NewZeland;
        List<Eruption>  elevetion2000 = eruptions.Where(e=> e.ElevationInMeters > 2000).ToList();
        List<Eruption> eruptionL = eruptions.Where(e=>e.Volcano.StartsWith("L")).ToList();
        int EruptionNr = eruptions.Where(e=> e.Volcano.StartsWith("L")).Count();
        int LarteriaMax =eruptions.Max(e => e.Year);
        Eruption MaxName= eruptions.FirstOrDefault(e => e.Year == LarteriaMax);
        List<Eruption> EruptionsAlph = eruptions.OrderBy(e=> e.Volcano).ToList();
        int SumEl =eruptions.Sum(e=> e.ElevationInMeters);
        bool erupt = eruptions.Any(e=> e.Year == 2000);
        List<Eruption> stratovolc = eruptions.Where(e=> e.Type == "Stratovolcano").Take(3).ToList();
        List<Eruption> AllEruption1000 = eruptions.Where(e=> e.Year <1000).OrderBy(e=> e.Volcano).ToList();
        List<string> NAmess = eruptions.Where(e => e.Year <1000).OrderBy(e=> e.Volcano).Select(e=> e.Volcano).ToList();
        
        
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
