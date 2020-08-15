using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DinnerSpinner.Api.Domain.Models
{
    public class Spinner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public ICollection<Dinner> Dinners { get; set; } = new List<Dinner>();
    }
}