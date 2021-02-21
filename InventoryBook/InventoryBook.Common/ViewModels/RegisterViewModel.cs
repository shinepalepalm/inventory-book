using InventoryBook.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryBook.Common.ViewModels
{
    public class RegisterViewModel
    {
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Remote("CheckLoginForRegistration", "Account", ErrorMessage = "Login already exists")]
        [Required(ErrorMessage = "You should input login")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You should input password")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Length should be between 6 and 50 characters")]

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "They are not equal")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public Role Role { get; set; }
    }
}
