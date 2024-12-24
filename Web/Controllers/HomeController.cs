using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Repository.Interface; // Add this to use your repository interface.

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPetTypeRepository _petTypeRepository;

    // Inject the repository through the constructor
    public HomeController(ILogger<HomeController> logger, IPetTypeRepository petTypeRepository)
    {
        _logger = logger;
        _petTypeRepository = petTypeRepository;
    }

    public IActionResult Index()
    {
        // Fetch all pet types
        var petTypes = _petTypeRepository.GetAll();

        // Pass the pet types to the view
        return View(petTypes);
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