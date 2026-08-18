using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    // TODO: apply [Authorize(Roles = "Owner")] — only the store owner should manage other admins.
    public class AdminsController : Controller
    {
        private static readonly List<AdminUser> _admins = new()
        {
            new AdminUser { Id = 1, FullName = "You", Email = "owner@yourstore.com", Role = "Owner", IsOwner = true, IsActive = true, LastActive = DateTime.Now },
            new AdminUser { Id = 2, FullName = "Usman Farooq", Email = "usman@yourstore.com", Role = "Collections Staff", IsOwner = false, IsActive = true, LastActive = DateTime.Now.AddHours(-3) },
        };

        public IActionResult Index()
        {
            ViewBag.StoreName = "Your Store";
            return View(_admins);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Invite()
        {
            // TODO: create pending invite, send email/SMS, persist AdminUser once accepted.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StoreName = "Your Store";
            var admin = _admins.Find(a => a.Id == id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        public IActionResult Revoke(int id)
        {
            var admin = _admins.Find(a => a.Id == id);
            if (admin != null) admin.IsActive = false;
            return RedirectToAction(nameof(Index));
        }
    }
}
