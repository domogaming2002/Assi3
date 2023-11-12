using DTO.DTO.Request.Order;
using DTO.DTO.Response.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Data;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        //Get: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<OrderResponseDTO>> GetOrders()
        {
            var orderList = orderRepository.GetOrders();
            return Ok(orderList);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderResponseDTO> GetOrderById(int id)
        {
            var order = orderRepository.GetOrderById(id);
            return Ok(order);
        }
        [HttpGet("OrderByMem/{id}")]
        public ActionResult<IEnumerable<OrderResponseDTO>> GetOrderByMemId(int id)
        {
            var order = orderRepository.GetOrderByMemId(id);
            return Ok(order);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddOrder(OrderAddDTO orderAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = orderRepository.AddOrder(orderAddDTO);
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            OrderUpdateDTO p = new OrderUpdateDTO()
            {
                OrderId = id,
            };
            var response = orderRepository.DeleteOrder(p);
            return Ok(response);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, OrderUpdateDTO orderUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            orderUpdateDTO.OrderId = id;
            var response = orderRepository.UpdateOrder(orderUpdateDTO);
            return Ok(response);
        }

        [HttpPost("AddOrderList")]
        public IActionResult AddOrder(OrderListAddDTO order)
        {
            var response = orderRepository.AddOrder(order);
            var orderLastAdd = orderRepository.GetLastIndex();
            foreach(var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = orderLastAdd.OrderId;
                var check = orderDetailRepository.AddOrderDetail(orderDetail);
            }
            return Ok(response);
        }
    }
}
