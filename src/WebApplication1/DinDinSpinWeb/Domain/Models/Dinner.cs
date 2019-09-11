using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{


        // public class Spinner
        // {
        //     public string Id { get; set; }

        //     public int TemperatureC { get; set; }

        //     public string Summary { get; set; }
        // }

        // public class Dinner : TableEntity
        // {
        //     public string SpinnerId { get; set; }

        //     public string Summary { get; set; }
        // }

    public class Spinner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
        
        public Spinner()
        {
            
        }

        public Spinner(string name)
        {
            Name = name;
        }
    }    

    public class Dinner
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SpinnerId")]
        public Spinner Spinner { get; set; }
        public int SpinnerId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public DateTime RegisterDate { get; set; }

        public Dinner()
        {
            
        }

        public Dinner(string name, DateTime registerDate)
        {
            Name = name;
            RegisterDate = registerDate;
        }
    }    

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
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