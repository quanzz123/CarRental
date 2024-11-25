using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DbRenalCarContext _context;

        public ProductController(DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var carList = _context.Cars.OrderBy(m => m.CarId).ToList();
            return View(carList);
        }
        public IActionResult Create()
        {
            var carType = (from c in _context.CarTypes 
                       select new SelectListItem ()
                       {
                           Text = c.CarTypeName,
                           Value = c.TypeId.ToString(),
                       }).ToList();
            carType.Insert(0, new SelectListItem()
            {
                Text = "--select--",
                Value = "0"
            });
            ViewBag.carType = carType;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car c)
        {
            if (ModelState.IsValid)
            {
                c.Alias = CarRental.Utilities.Function.TitleSlugGenerationAlias(c.CarName);
                _context.Cars.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var car = _context.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }
            var carType = (from c in _context.CarTypes
                           select new SelectListItem()
                           {
                               Text = c.CarTypeName,
                               Value = c.TypeId.ToString(),
                           }).ToList();
            carType.Insert(0, new SelectListItem()
            {
                Text = "--select--",
                Value = string.Empty,
            });
            ViewBag.carType = carType;
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car c)
        {
            if (ModelState.IsValid)
            {
                c.Alias = CarRental.Utilities.Function.TitleSlugGenerationAlias(c.CarName);

                _context.Cars.Update(c);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [HttpPost]
        public IActionResult Delete (int id)
        {
            var deleCar = _context.Cars.Find(id);
            if (deleCar == null) {
                return NotFound();
            }
            _context.Cars.Remove(deleCar);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
