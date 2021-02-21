using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common;
using InventoryBook.Common.ViewModels;
using InventoryBook.DAL.Repositories.Interfaces;
using System.Collections.Generic;

namespace InventoryBook.BLL.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public IEnumerable<CategoryViewModel> GetAll() => _categoryRepository.GetAll().ToCategoryViewModelList();
    }
}
