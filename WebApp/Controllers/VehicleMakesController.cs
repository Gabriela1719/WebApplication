using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.DataContext;
using DAL.Models;
using PagedList;

namespace WebApplication.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly DatabaseContext _context;

        public VehicleMakesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: VehicleMakes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var vehicleMake = from s in _context.VehicleMakes
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMake = vehicleMake.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMake = vehicleMake.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    vehicleMake = vehicleMake.OrderBy(s => s.Abrv);
                    break;
                default:
                    vehicleMake = vehicleMake.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(vehicleMake.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.ID))
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
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMakes.Any(e => e.ID == id);
        }
    }
}
