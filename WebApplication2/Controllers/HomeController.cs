using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
   
    
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }
    public IActionResult Age(DateTime birth, DateTime future)
    {
        ViewBag.birth = birth.ToString("yyyy-MM-dd");
        ViewBag.future = future.ToString("yyyy-MM-dd");
        
        if (future < birth)
        {
            ViewBag.ErrorMessage = "Niepoprawna data!";
            return View("CustomError");
        }

        int age = future.Year - birth.Year;

        if (future.Month < birth.Month || (future.Month == birth.Month && future.Day < birth.Day))
        {
            age--;
        }


        ViewBag.Result = age;

        return View();
    }
    
    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
       
        if (a is null || b is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b!";
            return View("CustomError");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "Nieznany operator!";
            return View("CustomError");
        }
        ViewBag.A = a;
        ViewBag.B = b;
        ViewBag.Operator = op;
        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = a + b;
                ViewBag.Operator = "+";
                break;
            case Operator.Sub :
                ViewBag.Result = a - b;
                ViewBag.Operator = "-";
                break;
            case Operator.Div:
                ViewBag.Result = a / b;
                ViewBag.Operator = ":";
                break;
            case Operator.Mul:
                ViewBag.Result = a * b;
                ViewBag.Operator = "*";
                break;
            default:
                ViewBag.ErrorMessage = "Nieznany operator!";
                return View("CustomError");
        }
        
        
        
      
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
public enum Operator
{
    Add, Sub, Mul, Div
}
