using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarImagesController : Controller
    {
        private readonly DbRenalCarContext _context;

        public CarImagesController(DbRenalCarContext context)
        {
            _context = context;
        }

        // GET: Admin/CarImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarImages.ToListAsync());
        }

        // GET: Admin/CarImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImage = await _context.CarImages
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (carImage == null)
            {
                return NotFound();
            }

            return View(carImage);
        }

        // GET: Admin/CarImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Image1,CarId,Image2,Image3,Image4")] CarImage carImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carImage);
        }

        // GET: Admin/CarImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImage = await _context.CarImages.FindAsync(id);
            if (carImage == null)
            {
                return NotFound();
            }
            return View(carImage);
        }

        // POST: Admin/CarImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Image1,CarId,Image2,Image3,Image4")] CarImage carImage)
        {
            if (id != carImage.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarImageExists(carImage.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carImage);
        }

        // GET: Admin/CarImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImage = await _context.CarImages
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (carImage == null)
            {
                return NotFound();
            }

            return View(carImage);
        }

        // POST: Admin/CarImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carImage = await _context.CarImages.FindAsync(id);
            if (carImage != null)
            {
                _context.CarImages.Remove(carImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarImageExists(int id)
        {
            return _context.CarImages.Any(e => e.ImageId == id);
        }
    }
}
