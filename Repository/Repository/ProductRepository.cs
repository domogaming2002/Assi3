using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DTO.DTO.Request.Product;
using DTO.DTO.Response.Product;
using Repository.IRepository;

namespace Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDAO _productDAO;
        IMapper _mapper;

        public ProductRepository(MyDBContext context, IMapper mapper)
        {
            _productDAO = new ProductDAO(context);
            _mapper = mapper;
        }

        public bool AddProduct(ProductAddDTO m)
        {
            return _productDAO.Create(_mapper.Map<Product>(m));
        }

        public bool DeleteProduct(ProductUpdateDTO p)
        {
            return _productDAO.Delete(_mapper.Map<Product>(p));
        }

        public ProductResponseDTO? GetProductById(int id)
        {
            return _mapper.Map<ProductResponseDTO?>(_productDAO.GetById(id));
        }

        public List<ProductResponseDTO>? GetProducts()
        {
            return _mapper.Map<List<ProductResponseDTO>>(_productDAO.GetProducts());
        }

        public bool UpdateProduct(ProductUpdateDTO p)
        {
            return _productDAO.Update(_mapper.Map<Product>(p));
        }
    }
}
