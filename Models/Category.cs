using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Category
    {
     
            public ObjectId Id { get; set; }

            public int CategoryId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }
    
    }
}
