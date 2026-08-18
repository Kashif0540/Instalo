using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class CustomersController : Controller
    {
        // TODO: replace with a real data store (EF Core DbContext, repository, etc.)
        private static readonly List<Customer> _customers = new()
        {
            new Customer { Id = 1, FullName = "Ahmed Raza", Phone = "0301-2345678", CnicNumber = "35201-1234567-1", Address = "Gulberg, Lahore", Status = "Active", GuarantorCount = 2, ActivePlanCount = 1, OutstandingBalance = 16500, CreatedOn = DateTime.Now.AddMonths(-3) },
            new Customer { Id = 2, FullName = "Bilal Hussain", Phone = "0301-9988776", CnicNumber = "35202-7654321-2", Address = "Model Town, Lahore", Status = "Overdue", GuarantorCount = 1, ActivePlanCount = 1, OutstandingBalance = 33800, CreatedOn = DateTime.Now.AddMonths(-5) },
            new Customer { Id = 3, FullName = "Sana Tariq", Phone = "0333-1122334", CnicNumber = "35203-1122334-5", Address = "Johar Town, Lahore", Status = "Active", GuarantorCount = 2, ActivePlanCount = 1, OutstandingBalance = 58200, CreatedOn = DateTime.Now.AddMonths(-1) },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_customers);
        }

        public IActionResult Create()
        {
            ViewBag.StoreName = "Your Store";
            return View(new CustomerCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: persist Customer + Guarantor(s) to the database.
            // At least Guarantor1 fields are required by the view model.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ViewBag.StoreName = "Your Store";
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StoreName = "Your Store";
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }
    }
}
