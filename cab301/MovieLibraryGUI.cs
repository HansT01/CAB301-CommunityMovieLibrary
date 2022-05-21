using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace cab301
{
    class MovieLibraryGUI
    {
        private readonly MovieCollection movies;
        private readonly MemberCollection members;

        public MovieLibraryGUI(MovieCollection movies, MemberCollection members)
        {
            this.movies = movies;
            this.members = members;
            while (MainMenu());
        }

        private void OptionSelect(string[] options, string option0 = null)
        {
            int[] indexRange = new int[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                indexRange[i] = i + 1;
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            if (option0 != null) Console.WriteLine($"0. {option0}");
            string option0Str = (option0 != null) ? "/0" : "";
            Console.WriteLine($"\nEnter your choice ==> ({string.Join("/", indexRange) + option0Str})");
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
                " "
            ));
            OptionSelect(new string[] { "Staff Login", "Member Login" }, "Exit");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    // Staff login
                    if (VerifyStaff())
                    {
                        while (StaffMenu()) ;
                    }
                    return true;
                case 2:
                    // Member login
                    if (members.Number <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("There is no register members in the system yet!");
                        Console.ReadLine();
                        return true;
                    }
                    IMember member = VerifyMember();
                    if (member != null)
                    {
                        while (MemberMenu(member)) ;
                    }
                    return true;
                case 0:
                    // Exit
                    return false;
                default:
                    // Re-render frame
                    return true;
            }
        }

        private bool StaffMenu()
        {
            Console.Clear();
            Console.WriteLine(string.Join(
                Environment.NewLine,
                "========================Staff Menu==========================",
                " "
            ));
            OptionSelect(new string[] {
                "Add new DVDs of a new movie to the system",
                "Remove DVDs of a movie from the system",
                "Register a new member with the system",
                "Remove a resitered member from the system",
                "Remove a resitered member from the system",
                "Display all members who are currently renting a particular movie"
            }, "Return to the main menu");
            switch (Convert.ToInt32(Console.ReadLine()))
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

        // TODO if movie already exist, this method should just increase available copies
        // instead of adding a new movie instance
        private bool AddMovie()
        {
            object SelectEnum(Type enumType)
            {
                string[] enums = Enum.GetNames(enumType);
                OptionSelect(enums);
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
                    " ",
                    $"{String.Join("\n", movie.ToString().Split(", "))}",
                    " "
                ));
                OptionSelect(new string[] {
                    "Add movie and exit menu",
                    "Redo input fields without adding movie"
                }, "Exit without adding movie");
                switch (int.Parse(Console.ReadLine())) {
                    case 1:
                        // Add movie and exit menu
                        if (movies.Insert(movie))
                        {
                            Console.WriteLine($"{copies} copies of '{movie.Title}' has been added to the collection");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to add '{movie.Title}' to the collection");
                        };
                        EnterToGoBack();
                        return false;
                    case 2:
                        // Redo input fields without adding movie
                        return true;
                    case 0:
                        return false;
                    default:
                        continue;
                }
            }
        }

        private bool MemberMenu(IMember member)
        {
            Console.Clear();
            Console.WriteLine(String.Join(
                Environment.NewLine,
                "======================Member Menu==========================",
                " "
            ));

            OptionSelect(new string[] {
                "Browse all the movies",
                "Display all the information about a movie, given the title of the movie",
                "Borrow a movie DVD",
                "Return a movie DVD",
                "List current borrowing movies",
                "Display the top 3 movies rented by the members",
            }, "Return to the main menu");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    // Browse all the movies
                    while (BrowseMovies()) ;
                    return true;
                case 2:
                    // Display all the information about a movie, given the title of the movie
                    while (DisplayMovieInformation()) ;
                    return true;
                case 3:
                    // Borrow a movie DVD
                    while (BorrowMovie(member)) ;
                    return true;
                case 4:
                    // Return a movie DVD
                    while (ReturnMovie(member)) ;
                    return true;
                case 5:
                    // List current borrowing movies
                    while (ListBorrowingMovies(member)) ;
                    return true;
                case 6:
                    // Display the top 3 movies rented by the members
                    while (ListTopMovies(3)) ;
                    return true;
                case 0:
                    // Return to the main menu
                    return false;
                default:
                    // Re-render menu
                    return true;
            }
        }

        // IComparer class that allows for comparing duplicate keys
        // https://stackoverflow.com/questions/5716423/c-sharp-sortable-collection-which-allows-duplicate-keys
        private class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
        {
            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);
                if (result == 0) return 1;
                return result;
            }
        }

        // Renders the top k movies by number of borrowings
        private bool ListTopMovies(int k)
        {
            Console.Clear();
            Console.WriteLine($"Top {k} movies rented by members: ");

            // ToArray() method has a time complexity of O(n)
            IMovie[] movieList = movies.ToArray();

            // SortedList insert method is O(nlog(m)), where m is the number of elements in the list
            // Since m is always less than k, this method has a time complexity of O(n)
            SortedList<int, IMovie> sortedList = new SortedList<int, IMovie>(new DuplicateKeyComparer<int>());
            for (int i = 0; i < movieList.Length; i++)
            {
                sortedList.Add(movieList[i].NoBorrowings, movieList[i]);
                if (sortedList.Count > k)
                {
                    // Remove the movie with the least borrowers
                    // Voodoo magic
                    sortedList.RemoveAt(0);
                }
            }

            // Get the top movies in descending order by borrower count
            IMovie[] topMovies = new IMovie[k];
            int iterator = k - 1;
            foreach (KeyValuePair<int, IMovie> pair in sortedList)
            {
                topMovies[iterator] = pair.Value;
                iterator--;
            }

            for (int i = 0; i < topMovies.Length; i++)
            {
                Console.WriteLine($"{i + 1}. '{topMovies[i].Title}' with {topMovies[i].NoBorrowings} borrowings");
            }

            EnterToGoBack();
            return false;
        }

        private bool ListBorrowingMovies(IMember member)
        {
            Console.Clear();
            Console.WriteLine("Movies the current users is currently borrowing: ");

            // ToArray() method has a time complexity of O(n)
            IMovie[] movieList = movies.ToArray();

            // Looping through the list of movies and searching for a member has a time complexity of O(nlog(m))
            // Where m is the number borrowers of a movie
            // This could easily be O(1) if borrowed movies were stored in the Member object
            // Or even O(n) if Members were stored in a hash map.
            IMovie[] borrowedMovies = new IMovie[movieList.Length];
            int count = 0;
            for (int i = 0; i < movieList.Length; i++)
            {
                if (movieList[i].Borrowers.Search(member))
                {
                    borrowedMovies[count] = movieList[i];
                    count++;
                }
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(borrowedMovies[i].ToString());
            }

            EnterToGoBack();
            return false;
        }

        private bool ReturnMovie(IMember member)
        {
            Console.Clear();
            Console.WriteLine("Enter the title of the movie you would like to return: ");
            string movieTitle = Console.ReadLine();

            IMovie movie = movies.Search(movieTitle);
            if (movie != null)
            {
                if (movie.RemoveBorrower(member))
                {
                    Console.WriteLine($"Removed member '{member.LastName}, {member.FirstName}' from '{movie.Title}'.");
                }
                else
                {
                    Console.WriteLine($"User '{member.LastName}, {member.FirstName}' does not have a copy of '{movie.Title}'.");
                }
            }
            else
            {
                Console.WriteLine($"No movie titled '{movieTitle}' was found.");
            }
            EnterToGoBack();
            return false;
        }

        private bool DisplayMovieInformation()
        {
            Console.Clear();
            Console.WriteLine("Enter the title of a movie: ");
            string movieTitle = Console.ReadLine();

            IMovie movie = movies.Search(movieTitle);
            if (movie != null)
            {
                string[] movieInfo = movie.ToString().Split(", ");
                foreach (string info in movieInfo)
                {
                    Console.WriteLine(info);
                }
            }
            EnterToGoBack();
            return false;
        }

        private bool BorrowMovie(IMember member)
        {
            Console.Clear();
            Console.WriteLine("Enter the title of a movie: ");
            string movieTitle = Console.ReadLine();

            IMovie movie = movies.Search(movieTitle);
            if (movie != null)
            {
                movie.AddBorrower(member);
                Console.WriteLine($"Borrowed a copy of '{movie.Title}' under the member '{member.LastName}, {member.FirstName}'.");
            }
            else
            {
                Console.WriteLine($"No movie titled '{movieTitle}' was found.");
            }
            EnterToGoBack();
            return false;
        }

        private bool BrowseMovies()
        {
            Console.Clear();
            IMovie[] movieList = movies.ToArray();
            for (int i = 0; i < movieList.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {movieList[i].ToString()}");
            }
            EnterToGoBack();
            return false;
        }

        // Verify if the member has correct credentials
        // Pre-condition: 
        // Post-condition: 
        private IMember VerifyMember()
        {
            Console.Clear();
            Console.WriteLine("Enter your name/username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("\nEnter your password: ");
            string password = Console.ReadLine();

            // Registered members are verified using their first name, last name and a password
            string[] name = userName.Split('\u0020');

            // Check if such a member exist
            IMember member = members.Find(new Member(name[0], name[1]));

            // If it does, check if the password entered is correct
            return (member != null && member.Pin == password) ? member : null;
        }

        // Verify if the staff username and passwords are correct
        private bool VerifyStaff()
        {
            Console.Clear();
            Console.WriteLine("Enter your name/username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("\nEnter your password: ");
            string password = Console.ReadLine();

            // Username and the password for the staff are ‘staff’ and ‘today123’, respectively
            return (userName == "staff" && password == "today123");
        }

        private void EnterToGoBack()
        {
            Console.WriteLine("\nPress enter to go back...");
            Console.ReadLine();
        }
    }
}
