using InventoryBook.Common;
using InventoryBook.Common.Enums;
using InventoryBook.Common.Models;
using InventoryBook.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryBook.DAL.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly ConnectionOptions _options;

        public ItemRepository(IOptions<ConnectionOptions> options) => _options = options.Value;

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.DeleteItemById, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ItemID", id);

                command.ExecuteNonQuery();
            }
        }

        public void Insert(Item item)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.InsertItem, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ItemName", item.Name);
                command.Parameters.AddWithValue("@Number", item.Number);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Condition", item.Condition);
                command.Parameters.AddWithValue("@CategoryName", item.Category.Name);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Item item)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.UpdateItem, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ItemID", item.Id);
                command.Parameters.AddWithValue("@ItemName", item.Name);
                command.Parameters.AddWithValue("@Number", item.Number);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Condition", item.Condition);
                command.Parameters.AddWithValue("@CategoryName", item.Category.Name);

                command.ExecuteNonQuery();
            }
        }

        public Item Get(int id)
        {
            Item item = null;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectItemById, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ItemID", id);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        item = new Item
                        {
                            Id = Convert.ToInt32(row["ItemID"]),
                            Name = row["Name"].ToString(),
                            Number = row["Number"].ToString(),
                            Description = row["Description"].ToString(),
                            Condition = (Condition)row["Condition"],
                            Category = new Category
                            {
                                Id = Convert.ToInt32(row["CategoryID"]),
                                Name = row["CategoryName"].ToString()
                            }
                        };
                    }
                }
            }

            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            var items = new List<Item>();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectAllItems, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        items.Add(new Item
                        {
                            Id = Convert.ToInt32(row["ItemID"]),
                            Name = row["Name"].ToString(),
                            Number = row["Number"].ToString(),
                            Description = row["Description"].ToString(),
                            Condition = (Condition)row["Condition"],
                            Category = new Category
                            {
                                Id = Convert.ToInt32(row["CategoryID"]),
                                Name = row["CategoryName"].ToString()
                            }
                        });
                    }
                }
            }

            return items;
        }

        public IEnumerable<Item> Filter(string text, ItemFilter filter)
        {
            var items = new List<Item>();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectItemsByFilter, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SearchText", text);
                command.Parameters.AddWithValue("@Filter", filter);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        items.Add(new Item
                        {
                            Id = Convert.ToInt32(row["ItemID"]),
                            Name = row["Name"].ToString(),
                            Number = row["Number"].ToString(),
                            Description = row["Description"].ToString(),
                            Condition = (Condition)row["Condition"],
                            Category = new Category
                            {
                                Id = Convert.ToInt32(row["CategoryID"]),
                                Name = row["CategoryName"].ToString()
                            }
                        });
                    }
                }
            }

            return items;
        }
    }
}
