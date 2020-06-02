using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        public static List<Product> Products = new List<Product> {
        new Product{Id=1,Name="Guitarra",Price=15000,Description="Guitarra negra"},
        new Product{Id=2,Name="Bajo",Price=15000,Description="Bajo Blanco"},
        new Product{Id=3,Name="Bateria",Price=15000,Description="Bateria Roja"},
        new Product{Id=4,Name="Microfono",Price=15000,Description="Microfono Azul"}
        };

        [HttpGet]
        public ActionResult<List<Product>> GetAll() {
            return Products;
        }

        [HttpGet("{Id}")]
        public ActionResult<Product> Get(int Id) {
            return Products.Single(x => x.Id == Id);
        }

        [HttpPost]
        public ActionResult Create(Product model) {
            model.Id = Products.Count() + 1;
            Products.Add(model);
            return CreatedAtAction("Get", new { id = model.Id }, model); //devuelve 201 created;
        }

        [HttpPut("{productId}")]
        public ActionResult Update(int productId, Product model) {
            var original = Products.Single(x => x.Id == productId);
            original.Name = model.Name;
            original.Price = model.Price;
            original.Description = model.Description;

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id) {
            Products = Products.Where(x => x.Id != Id).ToList();
            return NoContent();
        }

    }
}
