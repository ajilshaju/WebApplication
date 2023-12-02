using System;
using MongoDB.Bson;

namespace WebApplication.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public string Colour { get; set; }



    }
}
