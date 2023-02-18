using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Services
{
    // делает одну работу, возвращает перечисление продуктов
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        // комбинация адресов - корень сайта + ...
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

        // десериализация - json в объект Product, любое перечисление продуктов, например массив
        public IEnumerable<Product> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    // имя свойства нечувствительно к регистру
                    PropertyNameCaseInsensitive = true
                });
        }

        // добавление рейтинга - служебная функция
        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            // LINQ
            var query = products.First(x => x.Id == productId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            // запись в файл, на любом языке UTF-8
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }), 
                    products);
            }
        }
    }
}
