using DTO.DTO.Request.Product;
using DTO.DTO.Response.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        
        //Get: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseDTO>> GetProducts()
        {
            var productList = productRepository.GetProducts();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponseDTO> GetProductById(int id)
        {
            var product = productRepository.GetProductById(id);
            return Ok(product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct(ProductAddDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = productRepository.AddProduct(productDTO);
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            ProductUpdateDTO p = new ProductUpdateDTO()
            {
                ProductId = id,
            };
            var response = productRepository.DeleteProduct(p);
            return Ok(response);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")] 
        public IActionResult UpdateProduct(int id, ProductUpdateDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productDTO.ProductId = id;
            var response = productRepository.UpdateProduct(productDTO);
            return Ok(response);
        }


    }
}
