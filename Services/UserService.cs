using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Services
{
    public class UserService
    {
        private Database _database;

        public UserService()
        {
            _database = new Database();
        }


        //Add new user to the database
        public void AddUser(User user)
        {
            try
            {
                using (var connection = _database.GetConnection())
                {
                    connection.Open();

                    // SQL query to insert new user
                    var command = new SqlCommand("INSERT INTO Users (Name, SubscriptionType, SubscriptionEndDate) VALUES (@Name, @SubscriptionType, @SubscriptionEndDate)", connection);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@SubscriptionType", user.SubscriptionType);
                    command.Parameters.AddWithValue("@SubscriptionEndDate", user.SubscriptionEndDate);
                    
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the exception
                throw;
            }
        }


        // Retrieve and display all users from the database
        public void ViewUsers()
        {
            try
            {
                using (var connection = _database.GetConnection())
                {
                    connection.Open();

                    var command = new SqlCommand("SELECT * FROM Users", connection);
                    using (var reader = command.ExecuteReader()) // O(N) TimeComplexity 
                    {
                        while (reader.Read()) // O(N) TimeComplexity 
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Subscription Type: {reader["SubscriptionType"]}, Subscription End Date: {reader["SubscriptionEndDate"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the exception
                throw;
            }
        }

        // Retrieve and display user subscription details along with their borrowed books
        public void ViewSubscriptionDetails(int userId)
        {
            try
            {
                using (var connection = _database.GetConnection())
                {
                    connection.Open();

                    var command = new SqlCommand(@"
                        SELECT 
                            u.Name, 
                            u.SubscriptionType, 
                            u.SubscriptionEndDate, 
                            b.Title AS BookTitle
                        FROM 
                            Users u
                        LEFT JOIN 
                            BorrowedBooks bb ON u.Id = bb.UserId
                        LEFT JOIN 
                            Books b ON bb.BookId = b.Id
                        WHERE 
                            u.Id = @UserId AND (bb.IsReturned IS NULL OR bb.IsReturned = 0)", connection);

                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader()) // O(N) TimeComplexity 
                    {
                        // Check if the user exists
                        if (reader.Read())
                        {
                            Console.WriteLine($"Name: {reader["Name"]}");
                            Console.WriteLine($"Subscription Type: {reader["SubscriptionType"]}");
                            Console.WriteLine($"Subscription End Date: {reader["SubscriptionEndDate"]}");
                            Console.WriteLine("Borrowed Books:");

                            // Use HashSet to store unique book title
                            var bookTitles = new HashSet<string>();

                            // Loop through the result to collect book title
                            do
                            {
                                if (reader["BookTitle"] != DBNull.Value)
                                {
                                    bookTitles.Add(reader["BookTitle"].ToString());
                                }
                            } while (reader.Read()); // O(N) TimeComplexity 

                            // Print the collected unique book title
                            if (bookTitles.Count > 0)
                            {
                                foreach (var title in bookTitles)
                                {
                                    Console.WriteLine($"- {title}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Book Borrowed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                throw;
            }
        }
    }
}

