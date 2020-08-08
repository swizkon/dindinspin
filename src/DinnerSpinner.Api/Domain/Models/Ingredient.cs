using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DinnerSpinner.Api.Domain.Models
{
    public class Ingredient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool PlantBased { get; set; }
        
        public Ingredient()
        {
            
        }

        public Ingredient(string name)
        {
            Name = name;
        }
    }
}