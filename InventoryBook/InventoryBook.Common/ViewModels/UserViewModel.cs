using InventoryBook.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryBook.Common.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Remote("CheckLoginForUpdate", "User", AdditionalFields = nameof(Id), ErrorMessage = "Login already exists")]
        [Required(ErrorMessage = "You should input login")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Login { get; set; }

        public Role Role { get; set; }
    }
}
