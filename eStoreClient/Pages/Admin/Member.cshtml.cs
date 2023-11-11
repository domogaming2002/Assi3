using DTO.DTO.Request.Member;
using DTO.DTO.Response.Member;
using eBookStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class MemberModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiMember = "";

        public List<MemberResponseDTO> listMember { get; set; }
        public MemberModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiMember = Config.ApiUrl + "/Member";
        }

        public async Task GetData()
        {
            HttpResponseMessage response = await client.GetAsync(apiMember);
            string strData = await response.Content.ReadAsStringAsync();
            listMember = JsonConvert.DeserializeObject<List<MemberResponseDTO>>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task OnPostCreate(string email, string companyName, string city, string country, string password)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            MemberAddDTO member = new MemberAddDTO()
            {
                Email = email,
                CompanyName = companyName,
                City = city,
                Country = country,
                Password = password
            };
            var response = client.PostAsJsonAsync(apiMember, member).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();
        }

        public async Task OnPostDelete(int deleteMemberId)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = client.DeleteAsync(apiMember + "/" + deleteMemberId).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }

        public async Task OnPostUpdate(string updateEmail, string updateCompanyName, string updateCity, string updateCountry, string updatePassword, int updateMemberId)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            MemberUpdateDTO member = new MemberUpdateDTO()
            {
                MemberId = updateMemberId,
                Email = updateEmail,
                CompanyName = updateCompanyName,
                City = updateCity,
                Country = updateCountry,
                Password = updatePassword
            };
            var response = client.PutAsJsonAsync(apiMember + "/" + updateMemberId, member).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }
    }
}
