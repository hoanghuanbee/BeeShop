using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bee_Shop.Models;
using Bee_Shop.ViewModels;

public class AccountController : Controller
{
    private readonly BeeShopDbContext _context;

    public AccountController(BeeShopDbContext context)
    {
        _context = context;
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
}
