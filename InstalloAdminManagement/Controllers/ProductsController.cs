using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Samsung 43\" LED TV", Category = "Electronics", VendorName = "Al-Habib Electronics", CashPrice = 62000, InstallmentPrice = 71500, StockQuantity = 8, IsActive = true },
            new Product { Id = 2, Name = "Haier 12 CFT Refrigerator", Category = "Home Appliances", VendorName = "Al-Habib Electronics", CashPrice = 88000, InstallmentPrice = 99800, StockQuantity = 2, IsActive = true },
            new Product { Id = 3, Name = "Motorbike 70cc", Category = "Motorbikes", VendorName = "City Motors Distributors", CashPrice = 145000, InstallmentPrice = 168000, StockQuantity = 5, IsActive = true },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_products);
        }

        public IActionResult Create()
        {
            ViewBag.StoreName = "Your Store";
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: persist product to the database.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ViewBag.StoreName = "Your Store";
            var product = _products.Find(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StoreName = "Your Store";
            var product = _products.Find(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
