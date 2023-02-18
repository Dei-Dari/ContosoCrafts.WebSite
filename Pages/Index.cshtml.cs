using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        // logger предоставляет ASP.NET
        private readonly ILogger<IndexModel> _logger;
        // сервис продуктов
        public JsonFileProductService ProductService;
        // продукты - только получение, частный
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        // для страницы razor действие
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}
