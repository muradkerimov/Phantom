using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phantom.DAL;
using Phantom.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Phantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectsController : Controller
    {
        private readonly AppDbContext db;
        public ProjectsController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Projects");
            Project project = await db.Projects.FindAsync(id);
            if (project == null) return RedirectToAction("Index", "Projects");
            return View(project);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Project project)
        {
            if (!ModelState.IsValid) return View();
            string path = @"C:\Users\theka\source\repos\BackEnd-Projects\ForCourse\Phantom\Phantom\wwwroot\";
            string folder = @"images\";
            string fileName = Guid.NewGuid().ToString() + "-" + project.ImageFile.FileName;
            string finalPath = Path.Combine(path, folder, fileName);
            FileStream fileStream = new FileStream(finalPath, FileMode.Create);
            await project.ImageFile.CopyToAsync(fileStream);
            project.Image = fileName;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Projects");
            Project project = await db.Projects.FindAsync(id);
            if (project == null) return RedirectToAction("Index", "Projects");
            return View(project);
        }

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Projects");
            Project projectToDelete = await db.Projects.FindAsync(id);
            if (projectToDelete == null) return RedirectToAction("Index", "Projects");
            db.Projects.Remove(projectToDelete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Projects");
            Project project = await db.Projects.FindAsync(id);
            if (project == null) return RedirectToAction("Index", "Projects");
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Project project)
        {
            if (!ModelState.IsValid) return View();
            db.Projects.Update(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }
    }
}
