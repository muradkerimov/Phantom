using Microsoft.AspNetCore.Mvc;
using Phantom.DAL;
using Phantom.Models;
using System;
using System.Linq;

namespace Phantom.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _db;
        public ProjectController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? id)
        {
            Project project = _db.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null) return RedirectToAction("Index", "Home");
            return View(project);
        }

        public IActionResult AddProject()
        {
            return View();
        }
        public IActionResult AddProjectToDb(string title, string subtitle, string image, string description)
        {
            Project project = new Project();
            project.Title = title;
            project.SubTitle = subtitle;
            project.Description = description;
            project.Date = DateTime.Now;
            project.Image = image;
            _db.Projects.Add(project);
            _db.SaveChanges();
            TempData["projectAdded"] = "Your projet was added successfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
