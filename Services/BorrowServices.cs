using System;
using System.Data.SqlClient;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Services
{
    public class BorrowService
    {
        private Database _database;
        private BookService _bookService;
        
        public BorrowService()
        {
            _database = new Database();
            _bookService = new BookService();
        }

        // Method to borrow book
        public void BorrowBook(int userId, int bookId)
        {
            try
            {
                using (var connection = _database.GetConnection()) // O(1) SpaceComplexity 
                {
                    connection.Open(); // O(1) TimeComplexity 
                    
                    // Check if the book is available before borrowing
                    var checkAvailabilityCommand = new SqlCommand("SELECT IsAvailable FROM Books WHERE Id = @BookId", connection);
                    checkAvailabilityCommand.Parameters.AddWithValue("@BookId", bookId);
                    var isAvailable = (bool)checkAvailabilityCommand.ExecuteScalar();

                    if (!isAvailable)
                    {
                        Console.WriteLine("The book is not available for borrowing.");
                        return;
                    }
                    
                    // Insert the borrowing record
                    var command = new SqlCommand("INSERT INTO BorrowedBooks (UserId, BookId, BorrowDate, IsReturned) VALUES (@UserId, @BookId, @BorrowDate, 0)", connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                    command.ExecuteNonQuery();

                    // Mark the book as not available after borrow the book 
                    _bookService.MarkBookAsNotAvailable(bookId);

                    Console.WriteLine("Book borrowed successfully.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message); // Log the exception
                throw;
            }
        }

        // Method to return book
        public void ReturnBook(int userId, int bookId)
        {
            try
            {
                using (var connection = _database.GetConnection()) 
                {
                    connection.Open();

                    // Get the borrow record to check the borrow date
                    var borrowCommand = new SqlCommand("SELECT BorrowDate FROM BorrowedBooks WHERE UserId = @UserId AND BookId = @BookId AND IsReturned = 0", connection);
                    borrowCommand.Parameters.AddWithValue("@UserId", userId);
                    borrowCommand.Parameters.AddWithValue("@BookId", bookId);
                    
                    using (var reader = borrowCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime borrowDate = reader.GetDateTime(0);
                            DateTime dueDate = borrowDate.AddDays(14); // Assuming 14-day for borrowing period
                            DateTime returnDate = DateTime.Now;
                            
                            // Update the BorrowedBooks table and set IsReturned to true 
                            var updateCommand = new SqlCommand("UPDATE BorrowedBooks SET IsReturned = 1, ReturnDate = @ReturnDate WHERE UserId = @UserId AND BookId = @BookId AND IsReturned = 0", connection);
                            updateCommand.Parameters.AddWithValue("@UserId", userId);
                            updateCommand.Parameters.AddWithValue("@BookId", bookId);
                            updateCommand.Parameters.AddWithValue("@ReturnDate", returnDate);
                            updateCommand.ExecuteNonQuery();
                            
                            // Check if the return date is greater then due date
                            if (returnDate > dueDate) 
                            {
                                int daysOverdue = (returnDate - dueDate).Days;
                                decimal fineAmount = daysOverdue * 1.00m; // Assuming $1 fine per day
                                
                                // Insert fine record 
                                var fineCommand = new SqlCommand("INSERT INTO Fines (UserId, BookId, FineAmount, ReturnDate) VALUES (@UserId, @BookId, @FineAmount, @ReturnDate)", connection);
                                fineCommand.Parameters.AddWithValue("@UserId", userId);
                                fineCommand.Parameters.AddWithValue("@BookId", bookId);
                                fineCommand.Parameters.AddWithValue("@FineAmount", fineAmount);
                                fineCommand.Parameters.AddWithValue("@ReturnDate", returnDate);
                                fineCommand.ExecuteNonQuery();
                                
                                Console.WriteLine($"Book returned successfully. Fine of ${fineAmount} has been applied.");
                            }
                            else
                            {
                                Console.WriteLine("Book returned successfully. No fine applied.");
                            }
                            
                            // Make the book available again 
                            var makeAvailableCommand = new SqlCommand("UPDATE Books SET IsAvailable = 1 WHERE Id = @BookId", connection);
                            makeAvailableCommand.Parameters.AddWithValue("@BookId", bookId);
                            makeAvailableCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            Console.WriteLine("No matching record found for the return operation.");
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