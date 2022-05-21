using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace cab301
{
    class MovieLibraryGUI
    {
        MovieCollection movies;
        MemberCollection members;

        public MovieLibraryGUI()
        {
            this.movies = new MovieCollection();
            this.members = new MemberCollection(1000);
            while (MainMenu());
        }

        private bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine(string.Join(
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
                    while (!VerifyUser("staff"));
                    while (StaffMenu());
                    return true;
                case 2:
                    while (!VerifyUser("member")) ;
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
            Console.WriteLine(string.Join(
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
                    while (AddMovie());
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

        private bool AddMovie()
        {
            object SelectEnum(Type enumType)
            {
                string[] enums = Enum.GetNames(enumType);
                int[] indexRange = new int[enums.Length];
                for (int i = 0; i < enums.Length; i++)
                {
                    indexRange[i] = i + 1;
                    Console.WriteLine($"{i + 1}. {enums[i]}");
                }
                Console.WriteLine($"\nEnter your choice ==> ({string.Join("/", indexRange)})");
                try
                {
                    return Enum.Parse(enumType, enums[int.Parse(Console.ReadLine()) - 1]);
                }
                catch
                {
                    return null;
                }
            }

            Console.Clear();
            Console.WriteLine("Movie title: ");
            string title = Console.ReadLine();

            object genre = null;
            while (genre == null)
            {
                Console.Clear();
                Console.WriteLine("Select a genre: ");
                genre = SelectEnum(typeof(MovieGenre));
            }

            object classification = null;
            while (classification == null)
            {
                Console.Clear();
                Console.WriteLine("Select a classification: ");
                classification = SelectEnum(typeof(MovieClassification));
            }

            int duration = 0;
            while (duration <= 0)
            {
                Console.Clear();
                Console.WriteLine("Movie duration: ");
                try
                {
                    duration = int.Parse(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
            }

            int copies = 0;
            while (copies <= 0)
            {
                Console.Clear();
                Console.WriteLine("Total copies: ");
                try
                {
                    copies = int.Parse(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
            }

            Movie movie = new Movie(title, (MovieGenre) genre, (MovieClassification) classification, duration, copies);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(string.Join(
                    Environment.NewLine,
                    "Would you like to add the new movie:",
                    $"{movie.ToString()}",
                    " ",
                    "1. Add movie and exit menu",
                    "2. Redo input fields without adding movie",
                    "0. Exit without adding movie",
                    " ",
                    "Enter your choice ==> (1/2/0)"
                ));
                int input = int.Parse(Console.ReadLine());
                switch (input) {
                    case 1:
                        movies.Insert(movie);
                        return false;
                    case 2:
                        return true;
                    case 0:
                        return false;
                    default:
                        continue;
                }
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
                    while(BrowseMovies());
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

        private bool BrowseMovies()
        {
            Console.Clear();
            IMovie[] mList = movies.ToArray();
            for (int i = 0; i < mList.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {mList[i].ToString()}");
            }
            Console.WriteLine("\nPress enter to return to Member Menu: ");
            Console.ReadLine();
            return false;
        }

        // Verify if the user has correct the correct credential
        // Pre-condition: 
        // Post-condition: 
        private bool VerifyUser(string userType)
        {
            Console.Clear();
            Console.WriteLine("Enter your name/username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("\nEnter your password: ");
            string password = Console.ReadLine();

            if (userType == "staff")
            {
                // Username and the password for the staff are ‘staff’ and ‘today123’, respectively
                if (userName == "staff" && password == "today123")
                {
                    return true;
                }
                else return false;
            } else
            {
                // Registered members are verified using their first name, last name and a password
                string[] name = userName.Split('\u0020');
                IMember tempMember = new Member(name[0], name[1]);

                // Check if such a member exist
                if (!members.Search(tempMember))
                {
                    return false;
                }
                else
                {
                    // If it does, check if the password entered is correct
                    if (members.Find(tempMember).Pin == password)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
    }
}
