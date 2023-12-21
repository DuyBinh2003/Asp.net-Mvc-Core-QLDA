using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DoAn.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AccountController : Controller
    {
        private readonly CContext _context;

        public AccountController(CContext context)
        {
            _context = context;
        }

        // GET: /Authentication/Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: /Authentication/Account/SignUp
        [HttpPost]
        public IActionResult SignUp(string username, string email, string password, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Check if the username is unique
                if (_context.Users.Any(u => u.Username == username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View();
                }

                // Validate other conditions as needed

                // Create a new User object
                var user = new User
                {
                    Username = username,
                    Email = email,
                    Password = password, // Note: You may want to hash the password before saving it to the database
                    UserType = "1" // Set the default UserType to 1
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View();
        }



        // GET: /Authentication/Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Authentication/Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Store user information in session
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("UserType", user.UserType.ToString()); // Assuming UserType is an enumeration

                    // Check if the user is an admin
                    var isAdmin = (user.UserType == "admin"); 

                    if (isAdmin)
                    {
                        // Redirect to Admin area's HomeController/Index
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }

                    // Redirect to regular user's HomeController/Index
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View();
        }


    }
}
