using System;

namespace LibraryManagementSystem.Models
{
    // Represent book in the library System 
    public class Book
    {
        public int Id { get; set; } // Unique identifier for the book
        public string Title { get; set; } // Title of the book
        public string Author { get; set; } // Author of the book
        public bool IsAvailable { get; set; } // Availability status
    }
}