using AutoMapper;
using BusinessObject.Models;
using DTO.DTO.Request.Category;
using DTO.DTO.Request.Member;
using DTO.DTO.Request.Order;
using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Request.Product;
using DTO.DTO.Response.Category;
using DTO.DTO.Response.Member;
using DTO.DTO.Response.Order;
using DTO.DTO.Response.OrderDetail;
using DTO.DTO.Response.Product;

namespace Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductAddDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<Member, MemberAddDTO>().ReverseMap();
            CreateMap<Member, MemberUpdateDTO>().ReverseMap();
            CreateMap<Member, MemberResponseDTO>().ReverseMap();
            CreateMap<Member, MemberLogin>().ReverseMap();
            CreateMap<Order, OrderResponseDTO>().ReverseMap();
            CreateMap<Order, OrderAddDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderListAddDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponseDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailAddDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryAddDTO>().ReverseMap();
        }
    }
}
