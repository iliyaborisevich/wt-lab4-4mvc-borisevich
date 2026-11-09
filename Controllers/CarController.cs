using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_borisevich.Models;

namespace wt_lab4_4mvc_borisevich.Controllers;

public class CarController : Controller
{
    // Хранилище в памяти
    private static readonly List<Car> Cars = new()
    {
        new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2022, PricePerDay = 75, IsAvailable = true },
        new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2021, PricePerDay = 150, IsAvailable = true },
        new Car { Id = 3, Brand = "Kia", Model = "Rio", Year = 2020, PricePerDay = 45, IsAvailable = false }
    };

    // GET: /Car/Index
    public IActionResult Index()
    {
        return View(Cars);
    }

    // GET: /Car/Details/1
    public IActionResult Details(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound(); // обработка 404
        return View(car);
    }

    // GET: /Car/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Car/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Car car)
    {
        if (ModelState.IsValid)
        {
            car.Id = Cars.Any() ? Cars.Max(c => c.Id) + 1 : 1;
            Cars.Add(car);
            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    // GET: /Car/Edit/1 (Дополнительная операция)
    public IActionResult Edit(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();
        return View(car);
    }

    // POST: /Car/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Car car)
    {
        if (id != car.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var existing = Cars.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                existing.Brand = car.Brand;
                existing.Model = car.Model;
                existing.Year = car.Year;
                existing.PricePerDay = car.PricePerDay;
                existing.IsAvailable = car.IsAvailable;
                return RedirectToAction(nameof(Index));
            }
        }
        return View(car);
    }
}