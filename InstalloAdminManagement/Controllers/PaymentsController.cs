using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class PaymentsController : Controller
    {
        private static readonly List<InstallmentDue> _installments = new()
        {
            new InstallmentDue { Id = 1, CustomerName = "Ahmed Raza", CustomerPhone = "0301-2345678", ProductName = "Samsung 43\" LED TV", InstallmentNumber = 4, TotalInstallments = 6, DueDate = DateTime.Now.AddDays(2), AmountDue = 5500, LateFee = 0, Status = "Due" },
            new InstallmentDue { Id = 2, CustomerName = "Bilal Hussain", CustomerPhone = "0301-9988776", ProductName = "Haier 12 CFT Refrigerator", InstallmentNumber = 6, TotalInstallments = 12, DueDate = DateTime.Now.AddDays(-9), AmountDue = 4200, LateFee = 400, DaysLate = 9, Status = "Overdue" },
            new InstallmentDue { Id = 3, CustomerName = "Sana Tariq", CustomerPhone = "0333-1122334", ProductName = "Motorbike 70cc", InstallmentNumber = 3, TotalInstallments = 12, DueDate = DateTime.Now.AddDays(5), AmountDue = 9800, LateFee = 0, Status = "Due" },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            var model = new PaymentsPageViewModel
            {
                OverdueCount = _installments.Count(i => i.Status == "Overdue"),
                Installments = _installments
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Record()
        {
            // TODO: apply payment against the selected installment, recalc late fee, update Sale progress.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Receipt(int id)
        {
            ViewBag.StoreName = "Your Store";
            var installment = _installments.Find(i => i.Id == id);
            if (installment == null) return NotFound();
            return View(installment);
        }
    }
}
