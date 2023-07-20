using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HW6.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }

    //private readonly string _loginPath = "/Index";
    public IndexModel()
    {

    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        string username = Username;
        string password = Password;

        if (string.IsNullOrEmpty(username))
        {
            ModelState.AddModelError("LoginError", "Username is required");
            return Page();
        }

        if (string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("LoginError", "Password is required");
        }

        if (username == "intern" && password == "summer 2023 july")
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role,"User")};

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage();
        }
        else
        {
            ModelState.AddModelError("LoginError", "Invalid username or password");
            return Page();
        }

    }
    public async Task<IActionResult> OnPostHandleLogout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToPage();
    }
}
