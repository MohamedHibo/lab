using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;

namespace project.Controllers
{
    public class ProductController : Controller
    {
		ApplicationDbContext _context = new ApplicationDbContext();
		public IActionResult Index()
		{
			var products = _context.Products.Include(p => p.Category).ToList();
			return View(products);
		}

		public IActionResult Details(int id)
		{
			var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
			if (product == null) return NotFound();
			return View(product);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Products.Add(product);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var product = _context.Products.Find(id);
			if (product == null) return RedirectToAction("Index");

			ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Products.Update(product);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		[HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
