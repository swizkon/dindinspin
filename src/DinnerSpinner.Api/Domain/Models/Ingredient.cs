using MongoDB.Bson.Serialization.Attributes;

namespace DinnerSpinner.Api.Domain.Models
{
    public class Ingredient
    {
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