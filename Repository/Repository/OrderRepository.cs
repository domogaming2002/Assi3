using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DTO.DTO.Request.Order;
using DTO.DTO.Response.Order;
using Repository.IRepository;

namespace Repository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        OrderDAO _orderDAO;
        IMapper _mapper;

        public OrderRepository(MyDBContext context, IMapper mapper)
        {
            _orderDAO = new OrderDAO(context);
            _mapper = mapper;
        }

        public bool AddOrder(OrderAddDTO o)
        {
            return _orderDAO.Create(_mapper.Map<Order>(o));
        }

        public bool DeleteOrder(OrderUpdateDTO o)
        {
            return _orderDAO.Delete(_mapper.Map<Order>(o));
        }

        public OrderResponseDTO? GetOrderById(int id)
        {
            return _mapper.Map<OrderResponseDTO>(_orderDAO.GetById(id));
        }

        public List<OrderResponseDTO> GetOrderByMemId(int memId)
        {
            return _mapper.Map<List<OrderResponseDTO>>(_orderDAO.GetByMemberId(memId));
        }

        public List<OrderResponseDTO> GetOrders()
        {
            return _mapper.Map<List<OrderResponseDTO>>(_orderDAO.GetOrders());
        }

        public bool UpdateOrder(OrderUpdateDTO o)
        {
            return _orderDAO.Update(_mapper.Map<Order>(o));
        }
    }
}
