using System.ComponentModel.DataAnnotations;

namespace InventoryBook.Common.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        [Required(ErrorMessage = "You should input category name")]
        [StringLength(50, ErrorMessage = "Length should be less than 50 characters")]
        public string Name { get; set; }
    }
}
