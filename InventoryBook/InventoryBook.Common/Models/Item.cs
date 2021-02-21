using InventoryBook.Common.Enums;

namespace InventoryBook.Common.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public Condition Condition { get; set; }

        public Category Category { get; set; }
    }
}
