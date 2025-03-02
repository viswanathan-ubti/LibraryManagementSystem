using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class SubscriberUI
    {
        public void SubscriberMenu()
        {
            try
            {
                // Initialize services
                BookService bookService = new BookService(); // O(1) SpaceComplexity
                BorrowService borrowService = new BorrowService(); // O(1) SpaceComplexity
                UserService userService = new UserService(); // O(1) SpaceComplexity

                while (true)
                {
                    // Display subscriber menu options
                    Console.WriteLine("\nSubscriber Menu:");
                    Console.WriteLine("1. View Available Books");
                    Console.WriteLine("2. Borrow Book");
                    Console.WriteLine("3. Return Book");
                    Console.WriteLine("4. View Subscription Details");
                    Console.WriteLine("5. Exit");
                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine(); // O(1) TimeComplexity, O(1) SpaceComplexity

                    switch (choice)
                    {
                        case "1":
                            // View Available Books
                            Console.WriteLine("Available Books:");
                            bookService.ViewBooks();
                            break;

                        case "2":
                            // Borrow Book
                            int userId = PromptForInt("Enter your user ID: ");
                            int bookId = PromptForInt("Enter the book ID you want to borrow: ");
                            borrowService.BorrowBook(userId, bookId);
                            break;

                        case "3":
                            // Return Book
                            userId = PromptForInt("Enter your user ID: "); 
                            bookId = PromptForInt("Enter the book ID you want to return: "); 
                            borrowService.ReturnBook(userId, bookId);
                            break;

                        case "4":
                            // View Subscription Details
                            userId = PromptForInt("Enter your user ID: "); 
                            userService.ViewSubscriptionDetails(userId);
                            break;

                        case "5":
                            // Exit
                            return;

                        default:
                            Console.WriteLine("Invalid option Please try again");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle Exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Function to prompt user for integer input with validation
        static int PromptForInt(string message)
        {
            Console.Write(message);
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
            return result;
        }
    }
}