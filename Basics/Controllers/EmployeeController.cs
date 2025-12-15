using Basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers;

public class EmployeeController : Controller
{

    public IActionResult Index()
    {
        string message = $"Hello World. {DateTime.Now.ToString()}";

        // View'in dönüş parametreleri ?
        return View("Index", message);
    }

    public ViewResult Index1()
    {
        var names = new String[]
        {
            "Ahmet",
            "Mehmet",
            "Ayşe",
            "Fatma"
        };

        return View(names);
    }

    public IActionResult Index2()
    {
        var list = new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                FirstName = "Ahmet",
                LastName = "Yılmaz"
            },
            new Employee()
            {
                Id = 2,
                FirstName = "Mehmet",
                LastName = "Kara"
            },
            new Employee()
            {
                Id = 3,
                FirstName = "Muzaffer",
                LastName = "Boyacı"
            }
        };

        return View("Index2", list);
    }


    // HTML render olayı yok. Sadece string döndürür.
    public String Index3()
    {
        return "Hello World. This is Employee Controller";
    }

    // MVC’ye HTML sayfası render et diyor.
    public ViewResult Index4()
    {
        return View("Index3");
    }

    // MVC’de çok amaçlı return tipi.
    public IActionResult Index5()
    {
        return Content("Employee - Content() Mothod - IActionResult Return Type");
    }

    // Content() methodunun dönüş tipidir.
    public ContentResult Index6()
    {
        return Content("Employee - Content() Mothod - ContentResult Return Type");
    }

    /*
        Ve daha farklı pek çok return türü vardır.
    */
}