using System;
namespace LibraryManagementSystem.Models
{
    // Represent borrowed book in LibrarySystem
    public class BorrowedBook
    {
        public int Id { get; set; } // Unique identifier for the borrowed record
        public int UserId { get; set; } // ID of the user who borrowed the book
        public int BookId { get; set; } // ID of the borrowed book
        public DateTime BorrowDate { get; set; } // Date when the book was borrowed
        public DateTime? ReturnDate { get; set; } // Nullable date when the book is returned
    }
}