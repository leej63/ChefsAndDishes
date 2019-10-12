using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        [Key]
        [Required]
        public int ChefId { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required!")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [OverEighteen]
        public DateTime Birthday { get; set; }

        public List<Dish> CreatedDishes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class OverEighteen : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime today = DateTime.Now;
            if(today < (DateTime)value)
            {
                // DOB must be in past
                return new ValidationResult("Birthday field must be in the past.");
            }
            TimeSpan span = today - (DateTime)value;
            DateTime zeroTime = new DateTime(1,1,1);
            int years = (zeroTime + span).Year - 1;
            if (years < 18)
            {
                return new ValidationResult("Must be 18 or older to register as a chef.");
            }
            return ValidationResult.Success;
        }
    }
}