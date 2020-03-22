using System;
using System.Text.Json;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public CuisineType Cuisine { get; set; }
        public string ToJSON() => JsonSerializer.Serialize(this);
    }

}
