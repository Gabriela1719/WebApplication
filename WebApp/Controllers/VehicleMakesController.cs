using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.DataContext;
using DAL.Models;
using PagedList;
using LOGIC;

namespace WebApplication.Controllers
{
    public class VehicleMakesController : Controller
    {
        private IVehicleMakeRepository vehicleMakeRepository;

        public VehicleMakesController(IVehicleMakeRepository vehicleMakeRepository)
        {
            this.vehicleMakeRepository = vehicleMakeRepository;
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
            var vehicleMake = from s in vehicleMakeRepository.GetVehicleMakes()
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

            var vehicleMake = await vehicleMakeRepository.GetVehicleMakeByID(id);
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
                await vehicleMakeRepository.InsertVehicleMake(vehicleMake);
                vehicleMakeRepository.Save();
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

            var vehicleMake = await vehicleMakeRepository.GetVehicleMakeByID(id);
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
            try
            {
                if (ModelState.IsValid)
                {
                    await vehicleMakeRepository.UpdateVehicleMake(vehicleMake);
                    vehicleMakeRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
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

            var vehicleMake = await vehicleMakeRepository.GetVehicleMakeByID(id);
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
            var vehicleMake = await vehicleMakeRepository.GetVehicleMakeByID(id);
            await vehicleMakeRepository.DeleteVehicleMake(id);
            vehicleMakeRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
