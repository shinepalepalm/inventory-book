using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common;
using InventoryBook.Common.Enums;
using InventoryBook.Common.ViewModels;
using InventoryBook.DAL.Repositories.Interfaces;
using System.Collections.Generic;

namespace InventoryBook.BLL.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository) => _itemRepository = itemRepository;

        public void Create(ItemViewModel itemViewModel) => _itemRepository.Insert(itemViewModel.ToItem());

        public void Delete(int id) => _itemRepository.Delete(id);

        public void Edit(ItemViewModel itemViewModel) => _itemRepository.Update(itemViewModel.ToItem());

        public ItemViewModel Get(int id) => _itemRepository.Get(id)?.ToItemViewModel();

        public IEnumerable<ItemViewModel> GetAll() => _itemRepository.GetAll().ToItemViewModelList();

        public IEnumerable<ItemViewModel> Filter(string text, ItemFilter filter) => _itemRepository.Filter(text, filter).ToItemViewModelList();
    }
}
