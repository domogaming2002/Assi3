using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Response.OrderDetail;
using Repository.IRepository;

namespace Repository.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        OrderDetailDAO _orderDetailDAO;
        IMapper _mapper;

        public OrderDetailRepository(MyDBContext context, IMapper mapper)
        {
            _orderDetailDAO = new OrderDetailDAO(context);
            _mapper = mapper;
        }

        public bool AddOrderDetail(OrderDetailAddDTO orderDetail)
        {
            return _orderDetailDAO.Create(_mapper.Map<OrderDetail>(orderDetail));
        }

        public bool DeleteOrderDetail(OrderDetailUpdateDTO orderDetail)
        {
            return _orderDetailDAO.Delete(_mapper.Map<OrderDetail>(orderDetail));
        }

        public OrderDetailResponseDTO? GetOrderDetail(int orderId, int productId)
        {
            return _mapper.Map<OrderDetailResponseDTO>(_orderDetailDAO.GetByIdAll(orderId, productId));
        }

        public List<OrderDetailResponseDTO>? GetOrderDetailByOrderId(int orderId)
        {
            return _mapper.Map<List<OrderDetailResponseDTO>>(_orderDetailDAO.GetByIdOrder(orderId));
        }

        public List<OrderDetailResponseDTO> GetOrderDetails()
        {
            return _mapper.Map<List<OrderDetailResponseDTO>>(_orderDetailDAO.GetOrderDetails());
        }

        public bool UpdateOrderDetail(OrderDetailUpdateDTO orderDetail)
        {
            return _orderDetailDAO.Update(_mapper.Map<OrderDetail>(orderDetail));
        }
    }
}
