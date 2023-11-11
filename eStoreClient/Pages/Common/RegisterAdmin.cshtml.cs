using DTO.DTO.Request.Member;
using eBookStore.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace eBookStore.Pages.Common
{
    public class RegisterAdmin : PageModel
    {
        private readonly HttpClient client = null;
        private string api = "";

        public RegisterAdmin()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = Config.ApiUrl + "/Authen/Register/Admin";
        }


        public async Task OnGet()
        {
        }

        [BindProperty]
        public MemberRegister Users { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Users == null)
            {
                return Page();
            }

            var response = client.PostAsJsonAsync(api, Users).Result;
            var check = response.IsSuccessStatusCode;

            return RedirectToPage("./Login");
        }
    }
}
