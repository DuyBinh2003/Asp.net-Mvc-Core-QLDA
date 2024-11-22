using DoAn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DoAn.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AccountController : Controller
    {
        private readonly CContext _context;

        private readonly INotyfService _notyf;

        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(CContext context, INotyfService notyf, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _notyf = notyf;
            _httpClientFactory = httpClientFactory;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Tạo đối tượng user
            var user = new User
            {
                Username = username,
                Password = password // Mã hóa mật khẩu trước khi lưu trong thực tế
            };

            // Tạo dữ liệu JSON để gọi API
            var loginData = new LoginData
            {
                Username = username,
                Password = password
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            // Tạo HttpClient từ IHttpClientFactory
            var client = _httpClientFactory.CreateClient();

            // Gọi API để kiểm tra thông tin người dùng
            var apiResponse = await client.PostAsync("http://localhost:7147/api/account/login", jsonContent);

            if (apiResponse.IsSuccessStatusCode)
            {
                // Nếu API trả về thành công, lưu người dùng vào cơ sở dữ liệu
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Chuyển hướng người dùng đến trang chủ hoặc trang khác
                return Redirect("http://localhost:7147/");
            }
            else
            {
                _notyf.Error("Sign up unsuccessful");
                return View();
            }
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
