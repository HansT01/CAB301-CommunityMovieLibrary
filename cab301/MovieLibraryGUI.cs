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
            while (MainMenu()) ;
        }

        private void OptionSelect(string[] options, string option0 = null)
        {
            Console.WriteLine();
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
                "========================Main Menu==========================="
            ));
            OptionSelect(new string[] { "Staff Login", "Member Login" }, "Exit");
            switch (Console.ReadLine())
            {
                case "1":
                    // Staff login
                    if (VerifyStaff())
                    {
                        while (StaffMenu()) ;
                    }
                    return true;
                case "2":
                    // Member login
                    if (members.Number <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\nThere is no register members in the system yet!");
                        Console.ReadLine();
                        return true;
                    }
                    IMember member = VerifyMember();
                    if (member != null)
                    {
                        while (MemberMenu(member)) ;
                    }
                    return true;
                case "0":
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
            Console.WriteLine("========================Staff Menu==========================");
            OptionSelect(new string[] {
                "Add new DVDs of a new movie to the system",
                "Remove DVDs of a movie from the system",
                "Register a new member with the system",
                "Remove a registered member from the system",
                "Display a member's contact phone number, given the member's name",
                "Display all members who are currently renting a particular movie"
            }, "Return to the main menu");
            switch (Console.ReadLine())
            {
                case "1":
                    // Add new DVDs of a new movie to the system
                    while (AddMovie()) ;
                    return true;
                case "2":
                    // Remove DVDs of a movie from the system
                    while (DeleteMovie()) ;
                    return true;
                case "3":
                    // Register a new member with the system
                    while (RegisterMember()) ;
                    return true;
                case "4":
                    // Remove a resitered member from the system
                    while (RemoveMember()) ;
                    return true;
                case "5":
                    // Display a member's contact phone number, given the member's name
                    while (DisplayMemberPhoneNo()) ;
                    return true;
                case "6":
                    // Display all members who are currently renting a particular movie
                    while (DisplayBorrowers()) ;
                    return true;
                case "0":
                    // Return to the main menu
                    return false;
                default:
                    return true;
            }
        }

        // TODO code might need some cleaning up - will look into it later
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

            Movie movie = (Movie)movies.Search(title);
            if (movie != null)
            {
                Console.Clear();
                Console.WriteLine($"Movie with the title '{title}' already exists in the collection.");
                OptionSelect(new string[]
                {
                    "Add new copies of the movie",
                    "Enter a different movie title",
                }, "Return to staff menu");
                switch (Console.ReadLine())
                {
                    case "1":
                        // Add copies of the movie
                        Console.Clear();
                        Console.WriteLine("Enter in the number of copies to add: ");
                        try
                        {
                            int c = int.Parse(Console.ReadLine());
                            if (movie.AddCopies(c))
                            {
                                Console.WriteLine($"{c} copies of '{movie.Title}' has been successfully added");
                            }
                            else
                            {
                                Console.WriteLine("Please enter a positive integer");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Please enter a positive integer");
                        }
                        EnterToGoBack();
                        return false;
                    case "2":
                        // Enter a different movie title
                        return true;
                    case "0":
                        // Return to staff menu
                        return false;
                    default:
                        return true;
                }
            }

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

            movie = new Movie(title, (MovieGenre)genre, (MovieClassification)classification, duration, copies);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(string.Join(
                    Environment.NewLine,
                    "Would you like to add the new movie:",
                    " ",
                    $"{String.Join("\n", movie.ToString().Split(", "))}"
                ));
                OptionSelect(new string[] {
                    "Add movie and exit menu",
                    "Redo input fields without adding movie"
                }, "Exit without adding movie");
                switch (Console.ReadLine())
                {
                    case "1":
                        // Add movie and exit menu
                        if (movies.Insert(movie))
                        {
                            Console.WriteLine($"\n{copies} copies of '{movie.Title}' has been added to the collection");
                        }
                        else
                        {
                            Console.WriteLine($"\nFailed to add '{movie.Title}' to the collection");
                        };
                        EnterToGoBack();
                        return false;
                    case "2":
                        // Redo input fields without adding movie
                        return true;
                    case "0":
                        // Exit without adding movie
                        return false;
                    default:
                        continue;
                }
            }
        }

        // TODO code might need some cleaning up - will loo k into it later
        private bool DeleteMovie()
        {
            Console.Clear();
            Console.WriteLine("Enter the title: ");

            string title = Console.ReadLine();
            Movie movie = (Movie)movies.Search(title);

            if (movie != null)
            {
                Console.WriteLine("\n" + String.Join("\n", movie.ToString().Split(", ")));
                Console.WriteLine($"\nEnter the number of copies to remove from the movie: ");
                try
                {
                    int c = int.Parse(Console.ReadLine());
                    if (movie.RemoveCopies(c))
                    {
                        Console.WriteLine($"\nSuccessfully removed {c} copies from '{movie.Title}'");
                        if (movie.TotalCopies == 0)
                        {
                            movies.Delete(movie);
                            Console.WriteLine($"'{movie.Title}' has been removed from the library.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nUnable to remove {c} copies from '{movie.Title}'");
                    }
                }
                catch
                {
                    Console.WriteLine("\nPlease enter a positive integer");
                }
            }
            else
            {
                Console.WriteLine($"\nThere is no movie with the title '{title}' in the library.");
            }

            EnterToGoBack();
            return false;
        }

        private bool RegisterMember()
        {
            Console.Clear();
            Console.WriteLine("Enter the member's first name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("\nEnter the member's last name: ");
            string lastName = Console.ReadLine();

            if (!members.Search(new Member(firstName, lastName)))
            {
                string contactNumber = null;
                while (!IMember.IsValidContactNumber(contactNumber))
                {
                    Console.Clear();
                    Console.WriteLine("Enter a valid contact number: ");
                    contactNumber = Console.ReadLine();
                }

                string pin = null;
                while (!IMember.IsValidPin(pin))
                {
                    Console.Clear();
                    Console.WriteLine("Enter a valid pin: ");
                    pin = Console.ReadLine();
                }

                Member member = new(firstName, lastName, contactNumber, pin);
                while (true)
                {
                    member.ToString();
                    Console.Clear();
                    Console.WriteLine(string.Join(
                        Environment.NewLine,
                        "Would you like to add the new member:",
                        " ",
                        $"First name: {member.FirstName}",
                        $"Last name: {member.LastName}",
                        $"Contact number: {member.ContactNumber}",
                        $"Pin: {member.Pin}"
                    ));
                    OptionSelect(new string[] {
                        "Register member and exit menu",
                        "Redo input fields without registering member"
                    }, "Exit without registering member");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            // Register member and exit menu
                            if (!members.Search(member))
                            {
                                members.Add(member);
                                Console.WriteLine($"\n '{firstName}, {lastName}' has been registered as a new member.");
                            }
                            else
                            {
                                Console.WriteLine($"\n '{firstName}, {lastName}' is already registered.");
                            }
                            EnterToGoBack();
                            return false;
                        case "2":
                            // Redo input fields without registering member
                            return true;
                        case "0":
                            // Exit without registering member
                            return false;
                        default:
                            continue;
                    }
                }
            }

            return false;
        }

        private bool RemoveMember()
        {
            Console.Clear();
            Console.WriteLine("Enter the member's first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("\nEnter the member's last name: ");
            string lastName = Console.ReadLine();

            IMember member = members.Find(new Member(firstName, lastName));
            if (member == null)
            {
                Console.WriteLine($"\nMember {lastName}, {firstName} not found.");
            }
            else
            {
                members.Delete(member);
                Console.WriteLine($"\nMember {member.LastName}, {member.FirstName} has been removed.");
            }

            EnterToGoBack();
            return false;
        }

        private bool DisplayMemberPhoneNo()
        {
            Console.Clear();
            Console.WriteLine("Enter the member's first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("\nEnter the member's last name: ");
            string lastName = Console.ReadLine();

            IMember member = members.Find(new Member(firstName, lastName));
            if (member == null)
            {
                Console.WriteLine($"\nMember with the '{lastName}, {firstName}' not found.");
            }
            else
            {
                Console.WriteLine($"\nThe member's phone number is: '{member.ContactNumber}'");
            }

            EnterToGoBack();
            return false;
        }

        private bool DisplayBorrowers()
        {
            Console.Clear();
            Console.WriteLine("Enter the title of the movie you want to view: ");
            string title = Console.ReadLine();

            IMovie movie = movies.Search(title);
            if (movie != null)
            {
                Console.WriteLine("\n" + String.Join("\n", movies.Search(title).ToString().Split(", ")));
                Console.WriteLine($"\nThis movie is currently being borrowed by {movie.Borrowers.Number} members: ");
                Console.WriteLine(movies.Search(title).Borrowers.ToString());
            }
            else
            {
                Console.WriteLine("\nThere is no such movie in the collection.");
            }
            EnterToGoBack();
            return false;
        }
        private bool MemberMenu(IMember member)
        {
            Console.Clear();
            Console.WriteLine("======================Member Menu==========================");
            OptionSelect(new string[] {
                "Browse all the movies",
                "Display all the information about a movie, given the title of the movie",
                "Borrow a movie DVD",
                "Return a movie DVD",
                "List current borrowing movies",
                "Display the top 3 movies rented by the members",
            }, "Return to the main menu");
            switch (Console.ReadLine())
            {
                case "1":
                    // Browse all the movies
                    while (BrowseMovies()) ;
                    return true;
                case "2":
                    // Display all the information about a movie, given the title of the movie
                    while (DisplayMovieInformation()) ;
                    return true;
                case "3":
                    // Borrow a movie DVD
                    while (BorrowMovie(member)) ;
                    return true;
                case "4":
                    // Return a movie DVD
                    while (ReturnMovie(member)) ;
                    return true;
                case "5":
                    // List current borrowing movies
                    while (ListBorrowingMovies(member)) ;
                    return true;
                case "6":
                    // Display the top 3 movies rented by the members
                    while (ListTop3Movies()) ;
                    return true;
                case "0":
                    // Return to the main menu
                    return false;
                default:
                    // Re-render menu
                    return true;
            }
        }

        // Renders the top k movies by number of borrowings
        private bool ListTop3Movies()
        {
            Console.Clear();
            Console.WriteLine($"Top 3 movies rented by members: ");

            IMovie[] topMovies = TopMovies(movies.ToArray(), 3);

            for (int i = 0; i < topMovies.Length; i++)
            {
                Console.WriteLine($"{i + 1}. '{topMovies[i].Title}' with {topMovies[i].NoBorrowings} borrowings");
            }

            EnterToGoBack();
            return false;
        }

        private IMovie[] TopMovies(IMovie[] movies, int k)
        {
            // heap ← MinHeap<object>
            PriorityQueue<IMovie, int> queue = new();
            // for element in arr do
            for (int i = 0; i < movies.Length; i++)
            {
                // heap.insert(element)
                queue.Enqueue(movies[i], movies[i].NoBorrowings);
                // if heap.size > k
                if (queue.Count > k)
                {
                    // heap.delete()
                    queue.Dequeue();
                }
            }

            // topElements ← Object[heap.size]
            IMovie[] topMovies = new IMovie[queue.Count];
            // for i ← heap.size - 1 to 0 do
            for (int i = queue.Count - 1; i >= 0; i--)
            {
                // topElements[i] ← heap.peek()
                // heap.delete()
                topMovies[i] = queue.Dequeue();
            }

            // return topElements
            return topMovies;
        }

        private bool ListBorrowingMovies(IMember member)
        {
            Console.Clear();
            Console.WriteLine("Movies the current users is currently borrowing: \n");
            IMovieCollection borrowedMovies = ((Member)member).BorrowedMovies;

            IMovie[] movieList = borrowedMovies.ToArray();
            for (int i = 0; i < movieList.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {movieList[i].ToString()}");
            }

            EnterToGoBack();
            return false;
        }

        private bool ReturnMovie(IMember member)
        {
            Console.Clear();
            Console.WriteLine("Enter the title of the movie you would like to return: ");
            string movieTitle = Console.ReadLine();
            Console.WriteLine();

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
            Console.WriteLine();

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
            IMovieCollection borrowedMovies = ((Member)member).BorrowedMovies;

            if (borrowedMovies.Number < 5)
            {
                Console.WriteLine("Enter the title of a movie: ");
                string movieTitle = Console.ReadLine();
                IMovie movie = movies.Search(movieTitle);
                if (movie != null)
                {
                    movie.AddBorrower(member);
                    borrowedMovies.Insert(movie);
                    Console.WriteLine($"Borrowed a copy of '{movie.Title}' under the member '{member.LastName}, {member.FirstName}'.");
                }
                else
                {

                    Console.WriteLine($"No movie titled '{movieTitle}' was found.");
                }
            }
            else
            {
                Console.WriteLine("You cannot hold more than 5 DVDs at the same time.");
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
            // Registered members are verified using their first name, last name and a password
            Console.Clear();
            Console.WriteLine("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("\nEnter your last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("\nEnter your password: ");
            string password = Console.ReadLine();

            // Check if such a member exist
            IMember member = members.Find(new Member(firstName, lastName));

            // If it does, check if the password entered is correct
            // Scuffed security
            return (member != null && member.Pin == password) ? member : null;
        }

        // Verify if the staff username and passwords are correct
        private bool VerifyStaff()
        {
            Console.Clear();
            Console.WriteLine("Enter your username: ");
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
