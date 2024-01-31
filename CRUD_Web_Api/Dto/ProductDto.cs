using System.ComponentModel.DataAnnotations;

namespace CRUD_Web_Api.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }

    }

}
