using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // в конструктор передаем сервис
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        // свойство, место для хранения
        public JsonFileProductService ProductService { get; }

        // запрос как в начальной странице
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        //[HttpPatch] "[FromBody]"
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string ProducId,
            [FromQuery] int Rating)
        {
            ProductService.AddRating(ProducId, Rating);
            return Ok();
        }
    }
}
