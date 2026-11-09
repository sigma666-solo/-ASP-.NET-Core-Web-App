using System.Diagnostics;
using dependency_enjection.Data;
using Microsoft.AspNetCore.Mvc;
using dependency_enjection.Models;

namespace dependency_enjection.Controllers;

public class HomeController : Controller
{
    private readonly ProductRepository _repository;

    public HomeController(ProductRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var products = _repository.GetAll();
        return View(products);
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
