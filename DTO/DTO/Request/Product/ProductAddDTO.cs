namespace DTO.DTO.Request.Product
{
    public class ProductAddDTO
    {
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
    }
}
