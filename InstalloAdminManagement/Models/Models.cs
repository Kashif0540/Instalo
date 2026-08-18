using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstallmentAdminPanel.Models
{
    // ===================== Core Entities =====================

    public class Customer
    {
        public int Id { get; set; }

        [Required] public string FullName { get; set; }
        public string FatherOrHusbandName { get; set; }

        [Required, RegularExpression(@"^\d{5}-\d{7}-\d{1}$", ErrorMessage = "Format: XXXXX-XXXXXXX-X")]
        public string CnicNumber { get; set; }

        [Required, Phone] public string Phone { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public decimal MonthlyIncome { get; set; }

        public string Status { get; set; } = "Active"; // Active, Overdue, Cleared, Blacklisted
        public int GuarantorCount { get; set; }
        public int ActivePlanCount { get; set; }
        public decimal OutstandingBalance { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public List<Guarantor> Guarantors { get; set; } = new();
    }

    public class Guarantor
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Required] public string FullName { get; set; }
        public string Relationship { get; set; }
        public string CnicNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class Vendor
    {
        public int Id { get; set; }
        [Required] public string BusinessName { get; set; }
        public string ContactPerson { get; set; }
        [Phone] public string Phone { get; set; }
        [EmailAddress] public string Email { get; set; }
        public string Address { get; set; }
        public int ProductCount { get; set; }
        public decimal AmountOwed { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class Product
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Category { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal CashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int StockQuantity { get; set; }
        public int LowStockThreshold { get; set; } = 3;
        public bool IsActive { get; set; } = true;
    }

    public class InstallmentPlan
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMonths { get; set; }
        public string PaymentFrequency { get; set; } = "Monthly"; // Monthly, Bi-Weekly, Weekly
        public decimal DownPaymentPercent { get; set; }
        public decimal MarkupPercent { get; set; }
        public decimal LateFeeValue { get; set; }
        public int GracePeriodDays { get; set; }
        public bool IsActive { get; set; } = true;
        public int ActiveSalesCount { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int InstallmentPlanId { get; set; }
        public string PlanName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DownPaymentReceived { get; set; }
        public int TotalInstallments { get; set; }
        public int InstallmentsPaid { get; set; }
        public string Status { get; set; } = "Ongoing"; // Ongoing, Completed, Overdue
        public DateTime SaleDate { get; set; } = DateTime.Now;
    }

    public class InstallmentDue
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ProductName { get; set; }
        public int InstallmentNumber { get; set; }
        public int TotalInstallments { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
        public decimal LateFee { get; set; }
        public int DaysLate { get; set; }
        public string Status { get; set; } = "Due"; // Due, Overdue, Paid
    }

    public class AdminUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Owner, Manager, Sales Staff, Collections Staff
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime LastActive { get; set; } = DateTime.Now;
    }

    public class StoreSettings
    {
        public string StoreName { get; set; } = "Your Store";
        public string Currency { get; set; } = "PKR (Rs)";
        public string Address { get; set; }
        public string Phone { get; set; }

        public string LateFeeType { get; set; } = "Fixed"; // Fixed, Percentage
        public decimal LateFeeValue { get; set; }
        public int GracePeriodDays { get; set; } = 3;
        public string LateFeeRecurrence { get; set; } = "OneTime"; // OneTime, Daily, Weekly
        public bool AutoFlagDefaultAfterDays { get; set; } = true;
    }

    // ===================== View Models =====================

    public class DashboardViewModel
    {
        public decimal CollectedThisMonth { get; set; }
        public decimal CollectedDeltaPercent { get; set; }
        public decimal PendingDues { get; set; }
        public int PendingInstallmentCount { get; set; }
        public decimal OverdueAmount { get; set; }
        public int OverdueCustomerCount { get; set; }
        public int ActivePlanCount { get; set; }
        public int ActiveCustomerCount { get; set; }

        public List<InstallmentDue> DueThisWeek { get; set; } = new();
        public List<DefaulterRow> TopDefaulters { get; set; } = new();
        public List<ActivityRow> RecentActivity { get; set; } = new();
    }

    public class DefaulterRow
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public decimal OverdueAmount { get; set; }
        public int DaysLate { get; set; }
        public string GuarantorName { get; set; }
    }

    public class ActivityRow
    {
        public string Description { get; set; }
        public string Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CustomerCreateViewModel
    {
        [Required] public string FullName { get; set; }
        public string FatherOrHusbandName { get; set; }
        [Required] public string CnicNumber { get; set; }
        [Required, Phone] public string Phone { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public decimal MonthlyIncome { get; set; }

        [Required(ErrorMessage = "At least one guarantor is required.")]
        public string Guarantor1Name { get; set; }
        public string Guarantor1Relationship { get; set; }
        public string Guarantor1Cnic { get; set; }
        public string Guarantor1Phone { get; set; }
        public string Guarantor1Address { get; set; }

        public string Guarantor2Name { get; set; }
        public string Guarantor2Relationship { get; set; }
        public string Guarantor2Cnic { get; set; }
        public string Guarantor2Phone { get; set; }
        public string Guarantor2Address { get; set; }
    }

    public class SaleCreateViewModel
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int InstallmentPlanId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal DownPaymentReceived { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;

        public List<SelectListItem> Customers { get; set; } = new();
        public List<SelectListItem> Products { get; set; } = new();
        public List<SelectListItem> Plans { get; set; } = new();
    }

    public class PaymentsPageViewModel
    {
        public int OverdueCount { get; set; }
        public List<InstallmentDue> Installments { get; set; } = new();
    }

    public class ReportsViewModel
    {
        public decimal TotalMarkupIncome { get; set; }
        public decimal PrincipalCollected { get; set; }
        public decimal LateFeesCollected { get; set; }
        public decimal TotalOutstanding { get; set; }
        public List<VendorBreakdownRow> VendorBreakdown { get; set; } = new();
        public List<DefaulterRow> Defaulters { get; set; } = new();
    }

    public class VendorBreakdownRow
    {
        public string VendorName { get; set; }
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
        public int SharePercent { get; set; }
    }

    public class LoginViewModel
    {
        [Required] public string Username { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
