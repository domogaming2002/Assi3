using DTO.DTO.Request.Order;
using DTO.DTO.Response.Order;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
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
    }
}
