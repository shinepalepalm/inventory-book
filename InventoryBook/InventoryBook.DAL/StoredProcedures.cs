namespace InventoryBook.DAL
{
    public static class StoredProcedures
    {
        public const string DeleteUserById = "uspDeleteUserByID";
        public const string InsertUser = "uspInsertUser";
        public const string IsLoginExists = "uspIsLoginExists";
        public const string IsLoginExistsForUpdate = "uspIsLoginExistsForUpdate";
        public const string SelectAllUsers = "uspSelectUsers";
        public const string SelectUserById = "uspSelectUserByID";
        public const string SelectHashByLogin = "uspSelectHashByLogin";
        public const string SelectRoleForLogin = "uspSelectRoleForLogin";
        public const string SelectSaltByLogin = "uspSelectSaltByLogin";
        public const string UpdateUser = "uspUpdateUser";

        public const string DeleteItemById = "uspDeleteItemByID";
        public const string InsertItem = "uspInsertItem";
        public const string SelectAllItems = "uspSelectItems";
        public const string SelectItemById = "uspSelectItemByID";
        public const string SelectItemsByFilter = "uspSelectItemsByFilter";
        public const string UpdateItem = "uspUpdateItem";

        public const string SelectAllCategories = "uspSelectCategories";
    }
}
