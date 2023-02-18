using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Maker { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }
        //переопределение ToString
        // безумие на ракетном корабле => 
        // одна строка, без использования {}, через ракетный корабль  => в ToString, serialize возвращает строку - type string
        //public override ToString()
        //{
        //    JsonSerializer.Serialize<Product>(this) 
        //}
        public override string ToString() => JsonSerializer.Serialize<Product>(this);
    }
}
