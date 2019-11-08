using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinDinSpin.Domain.Models
{
    public class Dinner
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SpinnerId")]
        public Spinner Spinner { get; set; }
        public Guid SpinnerId { get; set; }

        [ForeignKey("MainIngredientId")]
        public Ingredient MainIngredient { get; set; }
        public int MainIngredientId { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public DateTime RegisterDate { get; set; }

        public Dinner()
        {

        }

        public Dinner(string name, DateTime registerDate)
        {
            Id = 123;
            Name = name;
            RegisterDate = registerDate;
        }
    }
}