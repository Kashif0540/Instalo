using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";

            // TODO: replace with real aggregation queries.
            var model = new ReportsViewModel
            {
                TotalMarkupIncome = 98600,
                PrincipalCollected = 342000,
                LateFeesCollected = 6200,
                TotalOutstanding = 612300,
                VendorBreakdown = new List<VendorBreakdownRow>
                {
                    new() { VendorName = "Al-Habib Electronics", UnitsSold = 22, Revenue = 412000, SharePercent = 62 },
                    new() { VendorName = "City Motors Distributors", UnitsSold = 6, Revenue = 254000, SharePercent = 38 },
                },
                Defaulters = new List<DefaulterRow>
                {
                    new() { CustomerName = "Bilal Hussain", OverdueAmount = 8400, DaysLate = 14, GuarantorName = "Kamran Hussain" },
                    new() { CustomerName = "Nasir Iqbal", OverdueAmount = 12600, DaysLate = 22, GuarantorName = "Fahad Iqbal" },
                }
            };

            return View(model);
        }

        public IActionResult Defaulters()
        {
            ViewBag.StoreName = "Your Store";
            return View();
        }
    }
}
