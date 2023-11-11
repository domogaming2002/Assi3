using DTO.DTO.Request.Member;
using eBookStore.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.User
{
    public class ProfileModel : PageModel
    {

        private readonly HttpClient client = null;
        private string apiMember = "";

        [BindProperty]
        public MemberUpdateDTO member { get; set; }

        public ProfileModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiMember = Config.ApiUrl + "/Member";
        }


        public async Task GetData()
        {
            int id = Int32.Parse(HttpContext.Session.GetString("id"));
            HttpResponseMessage response = await client.GetAsync(apiMember + "/" +  id);
            string strData = await response.Content.ReadAsStringAsync();
            member = JsonConvert.DeserializeObject<MemberUpdateDTO>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task OnPostUpdate(MemberUpdateDTO memberUpdateDTO)
        {
            var response = client.PutAsJsonAsync(apiMember + "/" + memberUpdateDTO.MemberId, memberUpdateDTO).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();
        }
    }
}
