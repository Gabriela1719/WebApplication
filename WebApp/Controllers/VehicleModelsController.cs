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
    public class VehicleModelsController : Controller
    {
        private IVehicleModelRepository vehicleModelRepository;

        public VehicleModelsController(IVehicleModelRepository vehicleModelRepository)
        {
            this.vehicleModelRepository = vehicleModelRepository;
        }

        // GET: VehicleModels
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
            var vehicleModel = from s in vehicleModelRepository.GetVehicleModles()
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModel = vehicleModel.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    vehicleModel = vehicleModel.OrderBy(s => s.Abrv);
                    break;
                default:
                    vehicleModel = vehicleModel.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(vehicleModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await vehicleModelRepository.GetVehicleModelByID(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await vehicleModelRepository.InsertVehicleModel(vehicleModel);
                vehicleModelRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await vehicleModelRepository.GetVehicleModelByID(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Abrv")] VehicleModel vehicleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await vehicleModelRepository.UpdateVehicleModel(vehicleModel);
                    vehicleModelRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await vehicleModelRepository.GetVehicleModelByID(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await vehicleModelRepository.GetVehicleModelByID(id);
            await vehicleModelRepository.DeleteVehicleModel(id);
            vehicleModelRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
