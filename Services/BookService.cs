using System;
using System.Data.SqlClient;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Services
{
    public class BookService
    {
        private Database _database;

        public BookService()
        {
            _database = new Database(); // O(1) SpaceComplexity
        }

        // Method to add book to the database
        public void AddBook(Book book)
        {
            try
            {
                using (var connection = _database.GetConnection()) // O(1) SpaceComplexity
                {
                    connection.Open(); // O(1) TimeComplexity
                    var command = new SqlCommand("INSERT INTO Books (Title, Author, IsAvailable) VALUES (@Title, @Author, @IsAvailable)", connection);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                    command.ExecuteNonQuery(); // O(1) TimeComplexity
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the error
                throw;
            }
        }

        // Method to view available books
        public void ViewBooks()
        {
            try
            {
                using (var connection = _database.GetConnection()) // O(1) SpaceComplexity
                {
                    connection.Open(); // O(1) TimeComplexity
                    var command = new SqlCommand("SELECT * FROM Books WHERE IsAvailable = 1", connection);
                    using (var reader = command.ExecuteReader()) // O(1) SpaceComplexity
                    {
                        while (reader.Read()) // O(n) TimeComplexity 
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Title: {reader["Title"]}, Author: {reader["Author"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the error
                throw;
            }
        }

        // Method to mark the book as not available
        public void MarkBookAsNotAvailable(int bookId)
        {
            try
            {
                using (var connection = _database.GetConnection()) // O(1) SpaceComplexity
                {
                    connection.Open(); // O(1) TimeComplexity 
                    var command = new SqlCommand("UPDATE Books SET IsAvailable = 0 WHERE Id = @BookId", connection);
                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.ExecuteNonQuery(); // O(1) TimeComplexity 
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the error
                throw;
            }
        }
    }
}