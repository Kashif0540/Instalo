using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class SalesController : Controller
    {
        private static readonly List<Sale> _sales = new()
        {
            new Sale { Id = 1001, CustomerName = "Ahmed Raza", ProductName = "Samsung 43\" LED TV", PlanName = "6 Months Standard", TotalPrice = 71500, TotalInstallments = 6, InstallmentsPaid = 3, Status = "Ongoing" },
            new Sale { Id = 1002, CustomerName = "Bilal Hussain", ProductName = "Haier 12 CFT Refrigerator", PlanName = "12 Months Extended", TotalPrice = 99800, TotalInstallments = 12, InstallmentsPaid = 5, Status = "Overdue" },
            new Sale { Id = 1003, CustomerName = "Sana Tariq", ProductName = "Motorbike 70cc", PlanName = "12 Months Extended", TotalPrice = 168000, TotalInstallments = 12, InstallmentsPaid = 2, Status = "Ongoing" },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_sales);
        }

        public IActionResult Create()
        {
            ViewBag.StoreName = "Your Store";

            // TODO: replace with real lookups from Customers / Products / InstallmentPlans tables.
            var model = new SaleCreateViewModel
            {
                Customers = new List<SelectListItem>
                {
                    new("Ahmed Raza", "1"), new("Bilal Hussain", "2"), new("Sana Tariq", "3")
                },
                Products = new List<SelectListItem>
                {
                    new("Samsung 43\" LED TV — Rs 71,500", "1"),
                    new("Haier 12 CFT Refrigerator — Rs 99,800", "2"),
                    new("Motorbike 70cc — Rs 168,000", "3"),
                },
                Plans = new List<SelectListItem>
                {
                    new("6 Months Standard (20% down, 12% markup)", "1"),
                    new("12 Months Extended (25% down, 18% markup)", "2"),
                    new("Weekly Quick Plan (15% down, 8% markup)", "3"),
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaleCreateViewModel model)
        {
            // TODO: validate customer has >=1 guarantor, calculate schedule, persist Sale + Installments.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ViewBag.StoreName = "Your Store";
            var sale = _sales.Find(s => s.Id == id);
            if (sale == null) return NotFound();
            return View(sale);
        }
    }
}
