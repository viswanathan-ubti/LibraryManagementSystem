# Library Management System

Library Management System is a C# console application that helps to manage book, users and borrowing records. It use MS SQL Server as the database.

## Features

- Admin Functions
   - Add new book (Allow admin to add book to library database)
   - Add new user (Enable the admin to register new user with subscription deatils)
   - View all user (Display all registered user)
   - View available books (Show list of books that are currently available for borrowing) 

- Subscriber Functions
    - View available books (Lets user see which books are available to borrow)
    - Borrow book (Allow user to borrow book if it is available)
    - Return book (Enable user to return borrowed book and check for fine)
    - View Subscription details (Display user name, subscription type, subscription expiry date and borrowed book name)
    - Pay fine for late return (Calculate and record the fine details)

- Additional Features
    - Logging system for tracking error
    - Exception handling


## Technologies Used

- Programming Language: **C#**
- Database: **MS SQL Server**
- Concepts Used:
    - Object Oriented Programming
    - Database Connectivity (ADO.NET)
    - Exception Handling
    - Logging Mechanism