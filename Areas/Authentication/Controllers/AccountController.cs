using DoAn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;
using MySqlX.XDevAPI;
using System.Xml;

namespace DoAn.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AccountController : Controller
    {
        private readonly CContext _context;

        private readonly INotyfService _notyf;

        public AccountController(CContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
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
                    _notyf.Error("Username already exists.");
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

                _notyf.Success("Sign up successful");
                return RedirectToAction("Login");
            }
            _notyf.Error("Sign up unsuccessful");
            return View();
        }



        // GET: /Authentication/Account/Login
        public IActionResult Login(string sessionId)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                // Gán session ID từ URL vào session
                HttpContext.Session.Clear(); // Xóa session cũ
                HttpContext.Session.SetString("SessionId", sessionId);
            }

            return View();
        }

        // POST: /Authentication/Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kiểm tra sessionId từ URL nếu có
            string sessionId = HttpContext.Session.GetString("SessionId");
            
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Nếu có sessionId từ URL, sử dụng nó thay vì tạo sessionId mới
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        HttpContext.Session.SetString("SessionId", sessionId);
                    }
                    else
                    {
                        // Tạo sessionId mới nếu không có sessionId từ URL
                        sessionId = Guid.NewGuid().ToString();
                        HttpContext.Session.SetString("SessionId", sessionId);
                    }

                    // Lưu các thông tin người dùng vào session
                    HttpContext.Session.SetInt32(sessionId + "_UserId", user.UserId);
                    HttpContext.Session.SetString(sessionId + "_Username", user.Username);
                    HttpContext.Session.SetString(sessionId + "_UserType", user.UserType.ToString());

                    // Kiểm tra xem người dùng là admin
                    var isAdmin = (user.UserType == "admin");

                    if (isAdmin)
                    {
                        // Chuyển hướng đến trang quản trị
                        _notyf.Success("Login successful as Admin");
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }

                    // Chuyển hướng đến trang chính của người dùng
                    _notyf.Success("Login successful");
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            _notyf.Error("Username or password invalid");
            return View();
        }


        //Sign out
        public IActionResult Logout()
        {
            // Clear user-related session values
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserType");

            // Redirect to the login page
            _notyf.Success("Logout successful");
            return RedirectToAction("Login", "Account", new { area = "Authentication" });
        }
    }
}
