using InventoryBook.Common.Enums;
using InventoryBook.Common.Models;
using System.Collections.Generic;

namespace InventoryBook.DAL.Repositories.Interfaces
{
    public interface IItemRepository
    {
        public void Delete(int id);

        public void Insert(Item item);

        public void Update(Item item);

        public Item Get(int id);

        public IEnumerable<Item> GetAll();

        public IEnumerable<Item> Filter(string text, ItemFilter filter);
    }
}
