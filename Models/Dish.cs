using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
	public class Dish
	{
        [Key]
        public int DishesId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chef is required")]
        public string Chef { get; set; }

        [Required(ErrorMessage = "Tastiness is required")]
        public int Tastiness { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Must be greater than 1, and less than 5000")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

