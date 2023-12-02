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
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var dbList = dbClient.GetDatabase("CatalogEdge").GetCollection<Category>("Category").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Category cat)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            int LastCategoryId = dbClient.GetDatabase("CatalogEdge").GetCollection<Category>("Category").AsQueryable().Count();
            cat.CategoryId = LastCategoryId + 1;

            dbClient.GetDatabase("CatalogEdge").GetCollection<Category>("Category").InsertOne(cat);


            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Category cat)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var filter = Builders<Category>.Filter.Eq("CategoryId", cat.CategoryId);

            var update = Builders<Category>.Update.Set("Name", cat.Name)

                                                   .Set("Description", cat.Description);



            dbClient.GetDatabase("CatalogEdge").GetCollection<Category>("Category").UpdateOne(filter, update);

            return new JsonResult("Updated Successfully");
        }


         [HttpDelete("{id}")]
         public JsonResult Delete(int id)
         {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ProductCatlogAppCon"));

            var filter = Builders<Category>.Filter.Eq("CategoryId", id);


             dbClient.GetDatabase("CatalogEdge").GetCollection<Category>("Category").DeleteOne(filter);

             return new JsonResult("Deleted Successfully");
         }



    }

}

