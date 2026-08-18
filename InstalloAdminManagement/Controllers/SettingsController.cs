using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class SettingsController : Controller
    {
        // TODO: load / save from the database, scoped to the current store.
        private static StoreSettings _settings = new();

        public IActionResult Index()
        {
            ViewBag.StoreName = _settings.StoreName;
            return View(_settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(StoreSettings model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            _settings = model;
            TempData["Success"] = "Settings saved.";
            return RedirectToAction(nameof(Index));
        }
    }
}
