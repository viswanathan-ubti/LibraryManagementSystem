using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class AdminUI
    {
        public void AdminMenu()
        {
            try
            {
                // Initialize services
                UserService userService = new UserService(); // O(1) SpaceComplexity
                BookService bookService = new BookService(); // O(1) SpaceComplexity
                BorrowService borrowService = new BorrowService(); // O(1) SpaceComplexity

                while (true)
                {
                    // Display admin menu options
                    Console.WriteLine("\nAdmin Menu:");
                    Console.WriteLine("1. Add User");
                    Console.WriteLine("2. Add Book");
                    Console.WriteLine("3. View Users");
                    Console.WriteLine("4. View Available Books");
                    Console.WriteLine("5. Exit");
                    Console.Write("Select an option: ");
                    string choice = Console.ReadLine(); // O(1) TimeComplexity, O(1) SpaceComplexity

                    switch (choice)
                    {
                        case "1":
                            // Add User
                            User newUser = new User
                            {
                                Name = PromptForString("Enter user name: "),  
                                SubscriptionType = PromptForString("Enter subscription type: "),
                                SubscriptionEndDate = DateTime.Now.AddMonths(1) 
                            };
                            userService.AddUser(newUser); 
                            Console.WriteLine("User added successfully");
                            break;

                        case "2":
                            // Add Book
                            Book newBook = new Book
                            {
                                Title = PromptForString("Enter book title: "),
                                Author = PromptForString("Enter book author: "),
                                IsAvailable = true 
                            };
                            bookService.AddBook(newBook);
                            Console.WriteLine("Book added successfully");
                            break;

                        case "3":
                            // View Users
                            Console.WriteLine("Users:");
                            userService.ViewUsers();
                            break;

                        case "4":
                            // View Available Books
                            Console.WriteLine("Available Books:");
                            bookService.ViewBooks(); 
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

        // Function to prompt user for string input
        static string PromptForString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}