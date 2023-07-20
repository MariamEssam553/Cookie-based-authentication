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
        else if (!(username == "intern" || string.IsNullOrEmpty(username)) && !(password == "summer 2023 july" || string.IsNullOrEmpty(password)))
        {
            ModelState.AddModelError("LoginError", "Invalid username or password");
            return Page();
        }

        return Page();
    }
    public async Task<IActionResult> OnPostHandleLogout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToPage();
    }
}
