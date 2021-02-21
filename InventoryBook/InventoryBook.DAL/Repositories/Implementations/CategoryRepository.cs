using InventoryBook.Common;
using InventoryBook.Common.Models;
using InventoryBook.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryBook.DAL.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ConnectionOptions _options;

        public CategoryRepository(IOptions<ConnectionOptions> options) => _options = options.Value;

        public IEnumerable<Category> GetAll()
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectAllCategories, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {

                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToInt32(row["CategoryID"]),
                            Name = row["Name"].ToString()
                        });
                    }
                }

            }

            return categories;
        }
    }
}
