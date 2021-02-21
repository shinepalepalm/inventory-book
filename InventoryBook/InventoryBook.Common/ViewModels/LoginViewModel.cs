using System.ComponentModel.DataAnnotations;

namespace InventoryBook.Common.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Required(ErrorMessage = "You should input login")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Required(ErrorMessage = "You should input password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Length should be between 6 and 50 characters")]
        public string Password { get; set; }
    }
}
