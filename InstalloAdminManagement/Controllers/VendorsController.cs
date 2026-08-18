using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class VendorsController : Controller
    {
        private static readonly List<Vendor> _vendors = new()
        {
            new Vendor { Id = 1, BusinessName = "Al-Habib Electronics", ContactPerson = "Rashid Mehmood", Phone = "042-1234567", ProductCount = 18, AmountOwed = 42000, IsActive = true },
            new Vendor { Id = 2, BusinessName = "City Motors Distributors", ContactPerson = "Waqas Ali", Phone = "042-7654321", ProductCount = 6, AmountOwed = 0, IsActive = true },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_vendors);
        }

        public IActionResult Create()
        {
            ViewBag.StoreName = "Your Store";
            return View(new Vendor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: persist vendor to the database.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ViewBag.StoreName = "Your Store";
            var vendor = _vendors.Find(v => v.Id == id);
            if (vendor == null) return NotFound();
            return View(vendor);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StoreName = "Your Store";
            var vendor = _vendors.Find(v => v.Id == id);
            if (vendor == null) return NotFound();
            return View(vendor);
        }
    }
}
