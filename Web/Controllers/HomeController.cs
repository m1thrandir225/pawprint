using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Repository.Interface; // Add this to use your repository interface.

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPetTypeRepository _petTypeRepository;
    private readonly IPetRepository _petRepository;

    // Inject the repository through the constructor
    public HomeController(ILogger<HomeController> logger, IPetTypeRepository petTypeRepository, IPetRepository petRepository)
    {
        _logger = logger;
        _petTypeRepository = petTypeRepository;
        _petRepository = petRepository;
    }

    public IActionResult Index()
    {
        var pets = _petRepository.GetAll(); // Synchronous call
        var petTypes = _petTypeRepository.GetAll(); // Synchronous call

        foreach (var pet in pets)
        {
            Console.WriteLine(pet.Name);
        }
        return View(pets);
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