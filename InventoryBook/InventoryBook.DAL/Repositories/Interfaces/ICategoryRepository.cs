using InventoryBook.Common.Models;
using System.Collections.Generic;

namespace InventoryBook.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
    }
}
