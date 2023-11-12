using DTO.DTO.Request.Order;
using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Response;
using DTO.DTO.Response.Category;
using eBookStore.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace eStoreClient.Pages.User
{
    public class CartModel : PageModel
    {
        private readonly HttpClient client = null;
        private string api = "";

        public CartModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = Config.ApiUrl + "/Order/AddOrderList";
        }
        public void OnGet()
        {
        }

        public async Task OnPostAdd(string cart)
        {
            if (!string.IsNullOrEmpty(cart))
            {
                var listProductDetail = JsonConvert.DeserializeObject<List<Cart>>(cart);
                decimal? money = 0;
                List<OrderDetailAddDTO> listOrderDetail = new List<OrderDetailAddDTO>(); 
                foreach(var pd in listProductDetail)
                {
                    OrderDetailAddDTO orderDetailAddDTO = new OrderDetailAddDTO()
                    {
                        ProductId = pd.ProductId,
                        UnitPrice = pd.UnitPrice,
                        Quantity = pd.Quantity,
                    };
                    money += pd.UnitPrice;
                    listOrderDetail.Add(orderDetailAddDTO);
                }
                ClaimsPrincipal claimUser = HttpContext.User;
                var test = claimUser.FindFirstValue(ClaimTypes.Sid);
                OrderListAddDTO orderListAddDTO = new OrderListAddDTO()
                {
                    MemberId = Int32.Parse(test),
                    OrderDate = DateTime.Now,
                    RequireDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    Freight = (decimal)money,
                    OrderDetails = listOrderDetail,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync(api, orderListAddDTO);
                string strData = await response.Content.ReadAsStringAsync();
            }
            var check = 0;
        }
    }
}
