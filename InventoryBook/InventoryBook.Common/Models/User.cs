﻿using InventoryBook.Common.Enums;

namespace InventoryBook.Common.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public Role Role { get; set; }
    }
}
