using DTO.DTO.Request.Product;
using DTO.DTO.Response.Product;

namespace Repository.IRepository
{
    public interface IProductRepository
    {
        bool AddProduct(ProductAddDTO m);
        ProductResponseDTO? GetProductById(int id);
        bool DeleteProduct(ProductUpdateDTO p);
        bool UpdateProduct(ProductUpdateDTO p);
        List<ProductResponseDTO>? GetProducts();
    }
}
