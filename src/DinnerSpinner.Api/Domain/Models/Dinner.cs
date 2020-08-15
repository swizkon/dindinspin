using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace DinnerSpinner.Api.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Dinner
    {
        // public int Id { get; set; }

        public Ingredient MainIngredient { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}