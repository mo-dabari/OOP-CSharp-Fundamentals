using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ThreeLayerArchitecture.BusinessLayer.Domains;
using ThreeLayerArchitecture.BusinessLayer.Enums;
using ThreeLayerArchitecture.DataAccessLayer.Interfaces;

namespace ThreeLayerArchitecture.DataAccessLayer.Concret
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public SqlCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Customer customer)
        {
            string query = "INSERT INTO customers (name, email) VALUES (@name, @email)";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@email", customer.Email);

                int rowAffect = command.ExecuteNonQuery();

                if (rowAffect != 1)
                    throw new InvalidOperationException("Failed to insert customer");
            }
        }

        public void Delete(Guid Id)
        {
            string query = "DELETE FROM customers WHERE id=@id";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@id", Id);

                int rowAffect = command.ExecuteNonQuery();

                if (rowAffect != 1)
                    throw new InvalidOperationException("Failed to delete customer");
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new();
            string query = "SELECT * FROM customers";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        customers.Add(MapToEntity(reader));
                }
            }
            return customers;
        }

        public Customer GetCustomerById(Guid Id)
        {
            string query = "SELECT * FROM customers WHERE id=@id";

            using SqlConnection connection = new(_connectionString);

            connection.Open();

            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@id", Id);

            using SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
                throw new InvalidOperationException($"Customer with id '{Id}' not found");

            return MapToEntity(reader);
        }

        public void Update(Customer customer)
        {
            string query = "UPDATE SET customers (name, email) VALUES(@name, @email) WHERE id=@id";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@id", customer.Id);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@email", customer.Email);

                int rowAffect = command.ExecuteNonQuery();

                if (rowAffect != 1)
                    throw new InvalidOperationException("Failed to update customer");
            }
        }

        private Customer MapToEntity(SqlDataReader reader)
        {
            string customerTypeValue = reader["customerType"]?.ToString() ?? string.Empty;

            if (!Enum.TryParse<enCustomerType>(customerTypeValue, ignoreCase: true, out var customerType))
                throw new InvalidOperationException($"Invalid customerType value in DB: {customerTypeValue}");

            return new Customer(
                reader["name"].ToString() ?? string.Empty,
                reader["email"].ToString() ?? string.Empty,
                customerType);

        }
    }
}
