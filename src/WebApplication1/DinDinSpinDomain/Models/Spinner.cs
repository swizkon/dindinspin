using System.ComponentModel.DataAnnotations;

namespace DinDinSpin.Domain.Models
{
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
}