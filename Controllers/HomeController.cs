using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    [HttpGet("")]
    public IActionResult IndexRedirect()
    {
        return RedirectToAction("Index");
    }

    [HttpGet("/dishes")]
    public IActionResult Index()
    {
        ViewBag.allDishes = _context.Dishes.ToList<Dish>();

        return View();
    }

    [HttpGet("dishes/createDish")]
    public IActionResult CreateDish()
    {
        return View("CreateDish");
    }

    [HttpPost("dishes/processDish")]
    public IActionResult ProcessDish(Dish dish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(dish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View("CreateDish");
    }

    [HttpGet("dishes/{dishId}")]
    public IActionResult OneDish(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishesId == dishId);

        return View("OneDish", dish);
    }

    [HttpGet("dishes/editDish/{dishId}")]
    public IActionResult EditDish(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishesId == dishId);

        return View("EditDish", dish);
    }

    [HttpPost("dishes/{dishId}/processEditDish")]
    public IActionResult ProcessEditDish(Dish dish1, int dishId)
    {

        Dish? dish2 = _context.Dishes.FirstOrDefault(d => d.DishesId == dishId);

        if(ModelState.IsValid)
        {
            dish2.Name = dish1.Name;
            dish2.Chef = dish1.Chef;
            dish2.Calories = dish1.Calories;
            dish2.Tastiness = dish1.Tastiness;
            dish2.Description = dish1.Description;

            _context.SaveChanges();

            return RedirectToAction("OneDish", new { dishId = dishId });
        }

        return View("EditDish", dish2);

    }

    [HttpPost("dishes/{dishId}/deleteDish")]
    public IActionResult DeleteDish(int dishId)
    {

        Dish? dishToDelete = _context.Dishes.FirstOrDefault(d => d.DishesId == dishId);

        _context.Dishes.Remove(dishToDelete);
        _context.SaveChanges();

        return RedirectToAction("Index");
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
