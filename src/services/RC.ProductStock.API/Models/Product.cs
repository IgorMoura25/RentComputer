using System;


namespace ProductStock.Models 
{

    public class Product 
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }  
    }
}