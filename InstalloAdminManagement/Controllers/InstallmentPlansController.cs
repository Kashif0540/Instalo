using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class InstallmentPlansController : Controller
    {
        private static readonly List<InstallmentPlan> _plans = new()
        {
            new InstallmentPlan { Id = 1, Name = "6 Months Standard", Description = "Most common plan for electronics.", DurationMonths = 6, PaymentFrequency = "Monthly", DownPaymentPercent = 20, MarkupPercent = 12, IsActive = true, ActiveSalesCount = 14 },
            new InstallmentPlan { Id = 2, Name = "12 Months Extended", Description = "For higher-value items like motorbikes.", DurationMonths = 12, PaymentFrequency = "Monthly", DownPaymentPercent = 25, MarkupPercent = 18, IsActive = true, ActiveSalesCount = 6 },
            new InstallmentPlan { Id = 3, Name = "Weekly Quick Plan", Description = "Short-term, smaller items.", DurationMonths = 3, PaymentFrequency = "Weekly", DownPaymentPercent = 15, MarkupPercent = 8, IsActive = true, ActiveSalesCount = 3 },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_plans);
        }

        public IActionResult Create()
        {
            ViewBag.StoreName = "Your Store";
            return View(new InstallmentPlan());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InstallmentPlan model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: persist plan template to the database.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StoreName = "Your Store";
            var plan = _plans.Find(p => p.Id == id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        public IActionResult Details(int id)
        {
            ViewBag.StoreName = "Your Store";
            var plan = _plans.Find(p => p.Id == id);
            if (plan == null) return NotFound();
            return View(plan);
        }
    }
}
