using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DinDinSpin.Domain.Models
{
    public class Spinner
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Dinner> Dinners { get; set; }
        
        public Spinner()
        {
            
        }

        public Spinner(string name)
        {
            Name = name;
        }
    }
}