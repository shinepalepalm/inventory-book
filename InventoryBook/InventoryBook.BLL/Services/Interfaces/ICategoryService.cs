using InventoryBook.Common.ViewModels;
using System.Collections.Generic;

namespace InventoryBook.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryViewModel> GetAll();
    }
}
