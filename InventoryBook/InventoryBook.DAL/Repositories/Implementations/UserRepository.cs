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
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionOptions _options;

        public UserRepository(IOptions<ConnectionOptions> options) => _options = options.Value;

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.DeleteUserById, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);

                command.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.UpdateUser, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", user.Id);
                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@Role", user.Role);

                command.ExecuteNonQuery();
            }
        }

        public void Register(string login, byte[] hash, byte[] salt, Role role)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.InsertUser, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@PasswordHash", hash);
                command.Parameters.AddWithValue("@PasswordSalt", salt);

                command.ExecuteNonQuery();
            }
        }

        public bool IsLoginExists(string login)
        {
            bool result;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.IsLoginExists, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Login", login);

                result = Convert.ToBoolean(command.ExecuteScalar());
            }

            return result;
        }

        public bool IsLoginExistsForUpdate(int id, string login)
        {
            bool result;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.IsLoginExistsForUpdate, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);
                command.Parameters.AddWithValue("@Login", login);

                result = Convert.ToBoolean(command.ExecuteScalar());
            }

            return result;
        }

        public byte[] GetHashByLogin(string login)
        {
            byte[] hash = null;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectHashByLogin, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Login", login);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        hash = row["PasswordHash"] as byte[];
                    }
                }
            }

            return hash;
        }

        public byte[] GetSaltByLogin(string login)
        {
            byte[] salt = null;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectSaltByLogin, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Login", login);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        salt = row["PasswordSalt"] as byte[];
                    }
                }
            }

            return salt;
        }

        public User Login(string login, byte[] passwordHash)
        {
            User user = null;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectRoleForLogin, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);


                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        user = new User
                        {
                            Id = Convert.ToInt32(row["UserID"]),
                            Login = login,
                            Role = (Role)row["Role"]
                        };
                    }
                }
            }

            return user;
        }

        public User Get(int id)
        {
            User user = null;

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectUserById, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        user = new User
                        {
                            Id = id,
                            Login = row["Login"].ToString(),
                            Role = (Role)row["Role"]
                        };
                    }
                }
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(StoredProcedures.SelectAllUsers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(row["UserID"]),
                            Login = row["Login"].ToString(),
                            Role = (Role)row["Role"]
                        });
                    }
                }
            }

            return users;
        }
    }
}
