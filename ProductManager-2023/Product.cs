using System;

namespace ProductManager_2023
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}