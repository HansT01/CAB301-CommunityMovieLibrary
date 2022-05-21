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

        }

        public static void MainMenu()
        {
            Console.Clear();
            string menuText = "============================================================\n"
                + "Welcome to Community Library Movie DVD Management System\n"
                + "============================================================\n"
                + "========================Main Menu===========================\n"
                + "1.Staff Login\n"
                + "2.Member Login\n"
                + "0.Exit\n"
                + "Enter your choice ==> (1/2/0)\n";
            Console.WriteLine(menuText);

            int input = Convert.ToInt32(Console.ReadLine());
            bool showMenu = true;
            while (showMenu)
            {
                switch (input)
                {
                    case 1:
                        StaffMenu();
                        break;
                    case 2:
                        MemberMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        showMenu = false;
                        break;
                }
            }
            MainMenu();
        }

        public static void StaffMenu()
        {
            Console.Clear();
            string menuText = "========================Stff Menu==========================\n"
                + "1.Add new DVDs of a new movie to the system\n"
                + "2.Remove DVDs of a movie from the system\n"
                + "3.Register a new member with the system\n"
                + "4.Remove a resitered member from the system\n"
                + "5.Remove a resitered member from the system\n"
                + "6.Display all members who are currently renting a particular movie\n"
                + "0.Return to the main menu\n"
                + "Enter your choice ==> (1/2/3/4/5/6/0)\n";
            Console.WriteLine(menuText);

            int input = Convert.ToInt32(Console.ReadLine());
            bool showMenu = true;
            while (showMenu)
            {
                switch (input)
                {
                    case 1:
                        //Add new DVDs of a new movie to the system
                        break;
                    case 2:
                        //Remove DVDs of a movie from the system
                        break;
                    case 3:
                        //Register a new member with the system
                        break;
                    case 4:
                        //Remove a resitered member from the system
                        break;
                    case 5:
                        //Remove a resitered member from the system
                        break;
                    case 6:
                        //Display all members who are currently renting a particular movie
                        break;
                    case 0:
                        //Return to the main menu
                        MainMenu();
                        break;
                    default:
                        showMenu = false;
                        break;
                }
            }
            StaffMenu();
        }

        public static void MemberMenu()
        {
            Console.Clear();
            string menuText = "======================Member Menu==========================\n"
                + "1.Browse all the movies\n"
                + "2.Display all the information about a movie, given the title of the movie\n"
                + "3.Borrow a movie DVD\n"
                + "4.Return a movie DVD\n"
                + "5.List current borrowing movies\n"
                + "6.Display the top 3 movies rented by the members\n"
                + "0.Return to the main menu\n"
                + "Enter your choice ==> (1/2/3/4/5/6/0)\n";
            Console.WriteLine(menuText);

            int input = Convert.ToInt32(Console.ReadLine());
            bool showMenu = true;
            while (showMenu)
            {
                switch (input)
                {
                    case 1:
                        //Browse all the movies
                        break;
                    case 2:
                        //Display all the information about a movie, given the title of the movie
                        break;
                    case 3:
                        //Borrow a movie DVD
                        break;
                    case 4:
                        //Return a movie DVD
                        break;
                    case 5:
                        //List current borrowing movies
                        break;
                    case 6:
                        //Display the top 3 movies rented by the members
                        break;
                    case 0:
                        //Return to the main menu
                        MainMenu();
                        break;
                    default:
                        showMenu = false;
                        break;
                }
            }
            MemberMenu();
        }

        //Verify if the user has correct the correct user name and password
        //Pre-condition: 
        //Post-condition: 
        public static bool verifyUser(string userType)
        {
            if (userType == "staff")
            {
                //username and the password for the staff are ‘staff’ and ‘today123’, respectively
            } else
            {
                //Registered members are verified using their first name, last name and a password
            }
            return true;
        }
    }
}
