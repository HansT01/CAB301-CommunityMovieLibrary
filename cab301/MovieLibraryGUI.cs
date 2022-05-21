using System;
using System.Collections.Generic;
using System.Text;

namespace cab301
{
    class MovieLibraryGUI
    {
        MovieCollection collection;

        public MovieLibraryGUI()
        {
            while (MainMenu());
        }

        private bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Join(
                Environment.NewLine,
                "============================================================",
                "Welcome to Community Library Movie DVD Management System",
                "============================================================",
                " ",
                "========================Main Menu===========================",
                " ",
                "1. Staff Login",
                "2. Member Login",
                "0. Exit",
                " ",
                "Enter your choice ==> (1/2/0)"
            ));

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    while (StaffMenu());
                    return true;
                case 2:
                    while (MemberMenu());
                    return true;
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        private bool StaffMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Join(
                Environment.NewLine,
                "========================Staff Menu==========================",
                " ",
                "1. Add new DVDs of a new movie to the system",
                "2. Remove DVDs of a movie from the system",
                "3. Register a new member with the system",
                "4. Remove a resitered member from the system",
                "5. Remove a resitered member from the system",
                "6. Display all members who are currently renting a particular movie",
                "0. Return to the main menu",
                " ",
                "Enter your choice ==> (1/2/3/4/5/6/0)"
            ));

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    // Add new DVDs of a new movie to the system
                    return true;
                case 2:
                    // Remove DVDs of a movie from the system
                    return true;
                case 3:
                    // Register a new member with the system
                    return true;
                case 4:
                    // Remove a resitered member from the system
                    return true;
                case 5:
                    // Remove a resitered member from the system
                    return true;
                case 6:
                    // Display all members who are currently renting a particular movie
                    return true;
                case 0:
                    // Return to the main menu
                    return false;
                default:
                    return true;
            }
        }

        private bool MemberMenu()
        {
            Console.Clear();
            Console.WriteLine(String.Join(
                Environment.NewLine,
                "======================Member Menu==========================",
                " ",
                "1. Browse all the movies",
                "2. Display all the information about a movie, given the title of the movie",
                "3. Borrow a movie DVD",
                "4. Return a movie DVD",
                "5. List current borrowing movies",
                "6. Display the top 3 movies rented by the members",
                "0. Return to the main menu",
                " ",
                "Enter your choice ==> (1/2/3/4/5/6/0)"
            ));

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    // Browse all the movies
                    return true;
                case 2:
                    // Display all the information about a movie, given the title of the movie
                    return true;
                case 3:
                    // Borrow a movie DVD
                    return true;
                case 4:
                    // Return a movie DVD
                    return true;
                case 5:
                    // List current borrowing movies
                    return true;
                case 6:
                    // Display the top 3 movies rented by the members
                    return true;
                case 0:
                    // Return to the main menu
                    return false;
                default:
                    return true;
            }
        }

        // Verify if the user has correct the correct user name and password
        // Pre-condition: 
        // Post-condition: 
        public bool VerifyUser(string userType)
        {
            if (userType == "staff")
            {
                // username and the password for the staff are ‘staff’ and ‘today123’, respectively
            } else
            {
                // Registered members are verified using their first name, last name and a password
            }
            return true;
        }
    }
}
