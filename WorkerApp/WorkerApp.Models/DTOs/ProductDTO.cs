using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerApp.Models.DTOs
{
    [Serializable]
    public class ProductDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int InStock { get; set; }
    }
}
