using Microsoft.AspNetCore.Mvc;
using Phantom.DAL;

namespace Phantom.Areas.Admin
{
    [Area("Admin")]
    public class DashboardController : Controller
    {

        private readonly AppDbContext db;
        public DashboardController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
