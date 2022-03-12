using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phantom.DAL;
using Phantom.Models;
using System.Threading.Tasks;

namespace Phantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderController : Controller
    {
        private readonly AppDbContext db;
        public HeaderController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Headers.FirstOrDefaultAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Header");
            Header header = await db.Headers.FindAsync(id);
            if (header == null) return RedirectToAction("Index", "Header");
            return View(header);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Header");
            await db.Headers.FindAsync(id);
            if (await db.Headers.FindAsync(id) == null) return RedirectToAction("Index", "Header");
            return View(await db.Headers.FirstOrDefaultAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Header header)
        {
            if (!ModelState.IsValid) return View();
            db.Headers.Update(header);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Header");
        }
    }
}
