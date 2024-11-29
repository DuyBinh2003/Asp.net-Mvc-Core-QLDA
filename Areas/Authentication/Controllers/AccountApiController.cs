using DoAn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DoAn.Areas.Authentication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly CContext _context;

        public AccountApiController(CContext context)
        {
            _context = context;
        }

        /// <summary>
        /// API Login - Xác thực người dùng
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Trạng thái thành công hoặc lỗi</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            // Tìm kiếm người dùng trong cơ sở dữ liệu
            var user = _context.Users.SingleOrDefault(u =>
                u.Username == loginRequest.Username &&
                u.Password == loginRequest.Password);

            if (user != null)
            {
                // Lưu thông tin người dùng vào Session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserType", user.UserType.ToString());


                return Ok(new
                {
                    message = "Login successful.",
                    session = HttpContext.Session.GetInt32("UserId")
                }) ;
               
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }


        /// <summary>
        /// API Logout - Xóa session người dùng
        /// </summary>
        /// <returns>Trạng thái thành công</returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Xóa session người dùng
            HttpContext.Session.Clear();
            return Ok(new { message = "Logout successful." });
        }
    }
}
