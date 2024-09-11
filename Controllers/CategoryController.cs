using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;

namespace project.Controllers
{
    public class CategoryController : Controller
    {
		ApplicationDbContext db = new ApplicationDbContext();
		public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
			ViewBag._products = new SelectList(db.Products, "ProductId", "Title"); 
			return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
			ModelState.Remove("Products");
			if (!ModelState.IsValid)
            {
				ModelState.AddModelError("", "All Fields Required");
				ViewBag._products = new SelectList(db.Products, "ProductId", "Title");
				return View();
			}
			db.Categories.Add(category);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Include(s => s.Products).FirstOrDefault(c => c.CategoryId == id);
			if (category == null) return RedirectToAction("Index");

			ViewBag._products = new SelectList(db.Products, "ProductId", "Title");
			return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
			//ModelState.Remove("Products");
			if (!ModelState.IsValid)
            {
				ModelState.AddModelError("", "All Fields Required");
				ViewBag._products = new SelectList(db.Products, "ProductId", "Title");
				return View();
            }
            db.Update(category);
            db.SaveChanges();
            return RedirectToAction("Index");
		}
	

	     public IActionResult Delete(int id)
	     {
		  var category = db.Categories.Find(id);
		  if (category == null) return RedirectToAction("Index");
		  db.Categories.Remove(category);
		  db.SaveChanges();
		   return RedirectToAction("Index");
		 }
    }
	
}

