using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Response.OrderDetail;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        IOrderDetailRepository orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }

        //Get: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetailResponseDTO>> GetOrders()
        {
            var orderDetailList = orderDetailRepository.GetOrderDetails();
            return Ok(orderDetailList);
        }

        [HttpGet("orderDetail")]
        public ActionResult<OrderDetailResponseDTO> GetOrderByAllId(int orderId, int productId)
        {
            var orderDetail = orderDetailRepository.GetOrderDetail(orderId, productId);
            return Ok(orderDetail);
        }

        [HttpGet("orderDetailByOrderId")]
        public ActionResult<OrderDetailResponseDTO> GetOrderByOrderId(int orderId)
        {
            var orderDetail = orderDetailRepository.GetOrderDetailByOrderId(orderId);
            return Ok(orderDetail);
        }


        [HttpPost("{orderId}")]
        public IActionResult AddOrderDetail(int orderId,OrderDetailAddDTO orderDetailAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            orderDetailAddDTO.OrderId = orderId;
            var response = orderDetailRepository.AddOrderDetail(orderDetailAddDTO);
            return Ok(response);
        }

        [HttpDelete("deleteOrderDetail")]
        public IActionResult DeleteOrderDetail(int orderId, int productId)
        {
            OrderDetailUpdateDTO p = new OrderDetailUpdateDTO()
            {
                OrderId = orderId,
                ProductId = productId

            };
            var response = orderDetailRepository.DeleteOrderDetail(p);
            return Ok(response);

        }

        [HttpPut("updateOrderDetail")]
        public IActionResult UpdateProduct(OrderDetailUpdateDTO orderDetailUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = orderDetailRepository.UpdateOrderDetail(orderDetailUpdateDTO);
            return Ok(response);
        }
    }
}
