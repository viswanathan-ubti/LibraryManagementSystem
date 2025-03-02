using System;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Data
{
    public class Database
    {
        // Connection string to connect to the SQL Server database
        private string connectionString = "Server=UIAP-S-TEST-01V;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=True;";

        // Method to establish and return SQL connection
        public SqlConnection GetConnection()
        {
            try
            {
                // Create new SQL connection object
                SqlConnection connection = new SqlConnection(connectionString); // O(1) SpaceComplexity
                return connection; // O(1) TimeComplexity
            }
            catch (Exception ex)
            {
                // Handle any error that may occur while creating the connection
                Console.WriteLine($"Error establishing database connection: {ex.Message}");
                return null; // Return null if connection fail
            }
        }
    }
}