using System.ComponentModel.DataAnnotations;

namespace Webapi.Models.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int InStock { get; set; }
    }
}
