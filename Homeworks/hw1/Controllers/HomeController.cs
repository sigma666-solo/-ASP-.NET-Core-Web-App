using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hw1.Models;

namespace hw1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string name, string surname)
    {
        ViewData ["Name"] = name;
        ViewData ["Surname"] = surname;
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
