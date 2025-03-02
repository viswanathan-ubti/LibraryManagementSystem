using System;
namespace LibraryManagementSystem.Models
{
    // Represent user in the library system
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user
        public string Name { get; set; } // Name of the user
        public string SubscriptionType { get; set; } // Subscription type of the user
        public DateTime SubscriptionEndDate { get; set; } // End date of the subscription
    }
}