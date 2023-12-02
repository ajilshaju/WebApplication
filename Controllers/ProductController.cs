using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using WebApplication.Models;
using Microsoft.AspNetCore.Http;



namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var dbList = dbClient.GetDatabase("CatalogEdge").GetCollection<Product>("Product").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Product prod)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            int LastProductId = dbClient.GetDatabase("CatalogEdge").GetCollection<Product>("Product").AsQueryable().Count();
            prod.ProductId = LastProductId + 1;

            dbClient.GetDatabase("CatalogEdge").GetCollection<Product>("Product").InsertOne(prod);


            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Product prod)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var filter = Builders<Product>.Filter.Eq("ProductId", prod.ProductId);

            var update = Builders<Product>.Update.Set("Name", prod.Name)

                                                   .Set("Description", prod.Description)

                                                   .Set("Category", prod.Category)

                                                   .Set("Price", prod.Price)

                                                   .Set("Colour", prod.Colour);


            dbClient.GetDatabase("CatalogEdge").GetCollection<Product>("Product").UpdateOne(filter, update);

            return new JsonResult("Updated Successfully");
        }


         [HttpDelete("{id}")]
         public JsonResult Delete(int id)
         {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var filter = Builders<Product>.Filter.Eq("ProductId", id);


             dbClient.GetDatabase("CatalogEdge").GetCollection<Product>("Product").DeleteOne(filter);

             return new JsonResult("Deleted Successfully");
         }



    }

}

