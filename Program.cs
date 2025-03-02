using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Display welcome message
                Console.WriteLine("Welcome to the Library Management System");

                // Initialize UI component for Admin and Subscriber
                AdminUI adminUI = new AdminUI();  // O(1) SpaceComplexity
                SubscriberUI subscriberUI = new SubscriberUI(); // O(1) SpaceComplexity

                while (true) // Infinite loop until user chooses to exit
                {
                    // Display role selection menu
                    Console.WriteLine("\nRole \n1. Admin \n2. Subscriber \n3. Exit");
                    Console.Write("Enter your Role: ");
                    string role = Console.ReadLine().ToLower(); // O(1) TimeComplexity, O(1) SpaceComplexity
                    
                    if (role == "1") // Check if user is Admin
                    {
                        adminUI.AdminMenu(); // Call Admin menu function
                        continue; 
                    }
                    else if (role == "2") // Check if user is Subscriber
                    {
                        subscriberUI.SubscriberMenu(); // Call Subscriber menu function 
                        continue;
                    }
                    else if (role == "3") // Check if user want to exit
                    {
                        return; // Exit program
                    }
                    else // Invalid input case
                    {
                        Console.WriteLine("Invalid role...Please restart the application and enter '1' for Admin or '2' for Subscriber.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle Exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}