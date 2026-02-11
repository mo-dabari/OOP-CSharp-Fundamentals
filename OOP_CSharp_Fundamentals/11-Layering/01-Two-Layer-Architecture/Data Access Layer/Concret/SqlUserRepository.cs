using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TwoLayerArchitecture.DataAccessLayer.Domain;
using TwoLayerArchitecture.DataAccessLayer.Interface;

namespace TwoLayerArchitecture.DataAccessLayer.Concret
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public SqlUserRepository(string connectionString)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));
            _connectionString = connectionString;
        }
        public bool Add(User user)
        {
            string query = @"INSERT INTO users (name) VALUES (@Name)";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("name", user.Name);

                int rowAffect = command.ExecuteNonQuery();

                return rowAffect > 0;
            }
        }

        public bool Delete(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            string query = @"DELETE FROM users WHERE name = @name";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("name", name);

                int rowAffect = command.ExecuteNonQuery();

                return rowAffect > 0;
            }
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new();
            string query = "SELECT * FROM users";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User(reader["name"].ToString() ?? string.Empty));

                    }
                }
            }
            return users;
        }

        public User GetById(string name)
        {
            string query = "SELECT * FROM users WHERE name = @name";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@name", name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return new User(reader["name"].ToString() ?? string.Empty);
                }
            }
            return null;
        }

        public bool Update(User user)
        {
            string query = "UPDATE FROM users set name=@name WHERE name=@name";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@name", user.Name);

                int rowAffect = command.ExecuteNonQuery();

                return rowAffect > 0;
            }
        }
    }
}
