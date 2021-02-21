using InventoryBook.Common.Enums;
using InventoryBook.Common.ViewModels;
using System.Collections.Generic;

namespace InventoryBook.BLL.Services.Interfaces
{
    public interface IItemService
    {
        public void Create(ItemViewModel itemViewModel);

        public void Delete(int id);

        public void Edit(ItemViewModel itemViewModel);

        public ItemViewModel Get(int id);

        public IEnumerable<ItemViewModel> GetAll();

        public IEnumerable<ItemViewModel> Filter(string text, ItemFilter filter);
    }
}
