using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsAndDishes.Models
{
    public class Dish
    {
        [Key]
        [Required]
        public int DishId { get; set; }

        [Required(ErrorMessage = "Name of dish is required!")]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter amount of calories of your dish.")]
        [Range(1,Int16.MaxValue)]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "Please describe your dish in a short short summary.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select between 1 & 5.")]
        [Range(1,6)]
        public int Tastiness { get; set; }

        [Required(ErrorMessage = "Please select the chef that created this dish.")]
        [ForeignKey("ChefId")]
        [Display(Name = "Chef")]
        public int ChefId { get; set; }

        public Chef Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}