using DTO.DTO.Request.Member;
using eBookStore.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using DTO.DTO.Response.Member;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectPRN_FAP.Pages.Common
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client = null;
        private string api = "";

        public LoginModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = Config.ApiUrl + "/Authen/login";
        }

        public MemberLogin loginDTO { get; set; }
        public bool IsLoginFailed { get; set; } = false;
        public IActionResult OnGet()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var test = claimUser.FindFirstValue(ClaimTypes.Role);
            if (claimUser.Identity.IsAuthenticated)
            {
                if (test == "Admin")
                {
                    return RedirectToPage("/Admin/Product");
                }
                else if (test == "Customer")
                {
                    return RedirectToPage("/User/ListProduct");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(MemberLogin loginDTO)
        {
            var jwt = await LoginAsync(loginDTO.Email, loginDTO.Password);
            if (!string.IsNullOrEmpty(jwt))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("jwt", jwt, cookieOptions);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
                if (jsonToken != null)
                {
                    var claims = jsonToken.Claims;
                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToPage("/Common/Login");
                }
            }
            return Page();

        }

        public async Task<IActionResult> OnPostLogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Common/Login");
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var loginRequest = new MemberLogin()
            {
                Email = email,
                Password = password
            };
            var response = await client.PostAsJsonAsync(api, loginRequest);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var jwt = JsonSerializer.Deserialize<MemberLoginResponse>(strData, options);
            return jwt.Token;
        }

    }
}
