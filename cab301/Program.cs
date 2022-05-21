using System;

namespace cab301
{
    class Program
    {
        static void Main(string[] args)
        {            
            MovieCollection movies = new MovieCollection();
            MemberCollection members = new MemberCollection(100);
            members.Add(new Member("Johnny", "Yang")
            {
                Pin = "1224",
                ContactNumber = "0469999999"
            });
            movies.Insert(new Movie("Action movie", MovieGenre.Action, MovieClassification.M15Plus, 150, 5));
            movies.Insert(new Movie("Comedy movie", MovieGenre.Comedy, MovieClassification.PG, 90, 25));
            movies.Insert(new Movie("Drama movie", MovieGenre.Drama, MovieClassification.PG, 120, 15));
            movies.Insert(new Movie("History movie", MovieGenre.History, MovieClassification.G, 60, 10));

            MovieLibraryGUI gui = new MovieLibraryGUI(movies, members);
        }
    }
}
