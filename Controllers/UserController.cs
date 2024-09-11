 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;

namespace project.Controllers
{
    public class UserController : Controller
    {
		ApplicationDbContext _context = new ApplicationDbContext();

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			
			if (_context.Users.Any(u => u.Email == user.Email))
			{
				ModelState.AddModelError("", "Email is already in use.");
				return View(user);
			}

			if (ModelState.IsValid)
			{
				

				_context.Users.Add(user);
				_context.SaveChanges();
				return RedirectToAction("Login");
			}

			return View(user);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			var user = _context.Users.SingleOrDefault(u => u.Email == email);
			if (user != null && user.Password == password)
			{
				return RedirectToAction("Index", "Product");
			}

			ModelState.AddModelError("", "Invalid login attempt.");
			return View();
		}

	}
}
