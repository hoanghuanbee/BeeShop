using Bee_Shop.Models;
using Bee_Shop.Services; // 👈 Thêm namespace chứa EmailSender
using Bee_Shop.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;
using System.Text.RegularExpressions;

public class AccountController : Controller
{
    private readonly BeeShopDbContext _context;
    private readonly EmailSender _emailSender; // ✅ Thêm dòng này

    public AccountController(BeeShopDbContext context, EmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender; // ✅ Gán injected service
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = _context.Users
            .FirstOrDefault(u =>
                (u.UserUsername == model.UsernameOrEmail || u.UserEmail == model.UsernameOrEmail)
                && u.UserPassword == model.Password);

        if (user != null)
        {
            // ✅ Chặn đăng nhập nếu chưa xác thực email
            if (user.Verified != 1)
            {
                ViewBag.Error = "Tài khoản của bạn chưa được xác nhận qua email.";
                return View(model);
            }

            var role = GetRoleName(user.RoleId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserUsername),
                new Claim("Role", role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Sai tài khoản, email hoặc mật khẩu.";
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    private string GetRoleName(int roleId)
    {
        return roleId switch
        {
            1 => "Admin",
            2 => "Warehouse",
            3 => "Order",
            4 => "Support",
            5 => "Accountant",
            6 => "Customer",
            _ => "Unknown"
        };
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        Console.WriteLine("✅ Bắt đầu Register");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("⚠️ ModelState không hợp lệ, dừng lại.");
            return View(model);
        }

        // ✅ Kiểm tra trùng username
        if (_context.Users.Any(u => u.UserUsername == model.Username))
        {
            ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
            return View(model); // ❗ Trả lại view luôn, không tiếp tục
        }

        // ✅ Kiểm tra trùng email
        if (_context.Users.Any(u => u.UserEmail == model.Email))
        {
            ModelState.AddModelError("Email", "Email đã tồn tại");
            return View(model);
        }

        // ✅ Kiểm tra định dạng mật khẩu
        if (!Regex.IsMatch(model.Password, @"^(?=.*[A-Z])(?=.*[\W_]).+$"))
        {
            ModelState.AddModelError("Password", "Mật khẩu phải có ít nhất 1 chữ hoa và 1 ký tự đặc biệt");
            return View(model);
        }

        // ✅ Kiểm tra khớp mật khẩu
        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "Nhập lại mật khẩu không khớp");
            return View(model);
        }

        // ✅ Nếu qua hết kiểm tra mới tạo user
        Console.WriteLine("✅ Tạo user và gửi email xác nhận...");

        var token = Guid.NewGuid().ToString();

        var user = new User
        {
            UserUsername = model.Username,
            UserEmail = model.Email,
            UserPassword = model.Password,
            RoleId = 6,
            Verified = 0,
            ConfirmationToken = token
        };

        Console.WriteLine("🧪 Trước khi save, Id = " + user.Id);
        _context.Users.Add(user);
        
        await _context.SaveChangesAsync();
        Console.WriteLine("✅ Sau khi save, Id = " + user.Id);
        var protocol = Request.IsHttps ? "https" : "http";
        var appUrl = $"{protocol}://{Request.Host}";
        await _emailSender.SendConfirmationEmail(appUrl, model.Email, token);

        ViewBag.Message = "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận tài khoản.";
        return View("ConfirmSuccess");


    }

    [HttpGet]
    public async Task<IActionResult> TestEmail()
    {
        var email = "huan12345ndnd@gmail.com";
        var token = Guid.NewGuid().ToString();

        var protocol = Request.IsHttps ? "https" : "http";
        var appUrl = $"{protocol}://{Request.Host}";
        await _emailSender.SendConfirmationEmail(appUrl, email, token);
        return Content("✅ Đã gửi email test!");
    }
    public IActionResult Confirm(string token)
    {
        Console.WriteLine("📩 Nhận token: " + token);

        var user = _context.Users.FirstOrDefault(u => u.ConfirmationToken == token);

        if (user == null)
        {
            ViewBag.Error = "Liên kết không hợp lệ hoặc đã hết hạn.";
            return View("ConfirmError");
        }

        user.Verified = 1;
        user.ConfirmationToken = null;
        _context.Users.Update(user);
        _context.SaveChanges();

        Console.WriteLine("✅ Tài khoản đã xác nhận: " + user.UserEmail);

        return View("ConfirmSuccess");
    }

    // GET: Giao diện nhập email để gửi lại xác nhận
    [HttpGet]
    public IActionResult ResendConfirmation()
    {
        return View();
    }

    // POST: Gửi lại email xác nhận
    [HttpPost]
    public async Task<IActionResult> ResendConfirmation(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);

        if (user == null || user.Verified == 1)
        {
            ViewBag.Message = "❌ Email không hợp lệ hoặc tài khoản đã xác nhận.";
            return View();
        }

        user.ConfirmationToken = Guid.NewGuid().ToString();
        await _context.SaveChangesAsync();

        var protocol = Request.IsHttps ? "https" : "http";
        var appUrl = $"{protocol}://{Request.Host}";
        await _emailSender.SendConfirmationEmail(appUrl, user.UserEmail, user.ConfirmationToken);

        ViewBag.Message = "✅ Email xác nhận đã được gửi lại. Vui lòng kiểm tra hộp thư.";
        return View();
    }

}
