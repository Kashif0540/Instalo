using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    // TODO: apply [Authorize(Roles = "Owner,Manager")] once auth is wired to real identity.
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.OwnerFirstName = "Owner";
            ViewBag.StoreName = "Your Store";

            // TODO: Replace with real aggregation queries from the database.
            var model = new DashboardViewModel
            {
                CollectedThisMonth = 284500,
                CollectedDeltaPercent = 12,
                PendingDues = 612300,
                PendingInstallmentCount = 96,
                OverdueAmount = 74200,
                OverdueCustomerCount = 9,
                ActivePlanCount = 3,
                ActiveCustomerCount = 58,
                DueThisWeek = new List<InstallmentDue>
                {
                    new() { CustomerName = "Ahmed Raza", ProductName = "Samsung 43\" LED", InstallmentNumber = 3, TotalInstallments = 6, DueDate = DateTime.Now.AddDays(1), AmountDue = 5500, Status = "Due" },
                    new() { CustomerName = "Bilal Hussain", ProductName = "Haier Refrigerator", InstallmentNumber = 5, TotalInstallments = 10, DueDate = DateTime.Now.AddDays(-2), AmountDue = 4200, Status = "Overdue" },
                    new() { CustomerName = "Sana Tariq", ProductName = "Motorbike 70cc", InstallmentNumber = 2, TotalInstallments = 12, DueDate = DateTime.Now.AddDays(3), AmountDue = 9800, Status = "Due" },
                },
                TopDefaulters = new List<DefaulterRow>
                {
                    new() { CustomerName = "Bilal Hussain", Phone = "0301-2345678", OverdueAmount = 8400, DaysLate = 14 },
                    new() { CustomerName = "Nasir Iqbal", Phone = "0333-1122334", OverdueAmount = 12600, DaysLate = 22 },
                },
                RecentActivity = new List<ActivityRow>
                {
                    new() { Description = "Payment collected from Ahmed Raza", Amount = "+Rs 5,500", Timestamp = DateTime.Now.AddHours(-2) },
                    new() { Description = "New installment sale created for Sana Tariq", Amount = "Rs 68,000", Timestamp = DateTime.Now.AddHours(-5) },
                    new() { Description = "Customer 'Kamran Sheikh' added", Timestamp = DateTime.Now.AddDays(-1) },
                }
            };

            return View(model);
        }
    }
}
