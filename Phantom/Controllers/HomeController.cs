using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phantom.DAL;
using Phantom.Models;
using Phantom.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Phantom.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Style = 0;
            HomeViewModel phantom = new HomeViewModel
            {
                headers = _db.Headers.FirstOrDefault(),
                projects = _db.Projects.ToList(),
            };
            return View(phantom);
        }
        public IActionResult addMessage(string name, string email, string message)
        {
            if (!ModelState.IsValid) return View();
            Message msg = new Message();
            msg.UserName = name;
            msg.UserEmail = email;
            msg.Text = message;
            msg.MessageDate = DateTime.Now;
            _db.Messages.Add(msg);
            _db.SaveChanges();
            TempData["messageAdded"] = "Your message was added successsfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
