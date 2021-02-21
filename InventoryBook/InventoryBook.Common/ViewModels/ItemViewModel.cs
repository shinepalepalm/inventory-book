using InventoryBook.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventoryBook.Common.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should input name")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Name { get; set; }

        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Required(ErrorMessage = "You should input number")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Number { get; set; }

        [Required(ErrorMessage = "You should input description")]
        public string Description { get; set; }

        public Condition Condition { get; set; }

        public CategoryViewModel CategoryViewModel { get; set; }
    }
}
