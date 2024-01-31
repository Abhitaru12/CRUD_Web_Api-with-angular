using System.ComponentModel.DataAnnotations;

namespace CRUD_Web_Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Range(1,1000)]
        public int Price { get; set; }
        [Range(1,40)]
        public int Qty { get; set; }


    }
}
