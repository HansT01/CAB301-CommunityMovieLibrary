using System;

namespace cab301_a1
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestMovie();
            TestMovieCollection();
        }

        private static void TestMovie()
        {
            Console.WriteLine("Creating movie with 3 available copies");
            Movie testMovie = new Movie("A", MovieGenre.Action, MovieClassification.G, 120, 3);
            Console.WriteLine(testMovie.ToString());

            Console.WriteLine("Adding A, B, B, C, D");
            Console.WriteLine(testMovie.AddBorrower(new Member("A", "A")));
            Console.WriteLine(testMovie.AddBorrower(new Member("B", "B")));
            Console.WriteLine(testMovie.AddBorrower(new Member("B", "B")));
            Console.WriteLine(testMovie.AddBorrower(new Member("C", "C")));
            Console.WriteLine(testMovie.AddBorrower(new Member("D", "D")));

            Console.WriteLine("Removing D, B, B");
            Console.WriteLine(testMovie.RemoveBorrower(new Member("D", "D")));
            Console.WriteLine(testMovie.RemoveBorrower(new Member("B", "B")));
            Console.WriteLine(testMovie.RemoveBorrower(new Member("B", "B")));

            Console.WriteLine("Adding D, E");
            Console.WriteLine(testMovie.AddBorrower(new Member("D", "D")));
            Console.WriteLine(testMovie.AddBorrower(new Member("E", "E")));
            Console.WriteLine(testMovie.ToString());

            Console.WriteLine("Removing A and adding null");
            Console.WriteLine(testMovie.RemoveBorrower(new Member("A", "A")));
            Console.WriteLine(testMovie.AddBorrower(null));
            Console.WriteLine(testMovie.ToString());

            Console.WriteLine("Removing null");
            Console.WriteLine(testMovie.RemoveBorrower(null));
            Console.WriteLine(testMovie.ToString());
        }

        private static void TestMovieCollection()
        {
            MovieCollection collection = new MovieCollection();
            IMovie[] movies;
            IMovie movie;

            Console.WriteLine("Adding movies in random order from A to M");
            collection.Insert(new Movie("K"));
            collection.Insert(new Movie("L"));
            collection.Insert(new Movie("J"));
            collection.Insert(new Movie("H"));
            collection.Insert(new Movie("E"));
            collection.Insert(new Movie("F"));
            collection.Insert(new Movie("C"));
            collection.Insert(new Movie("G"));
            collection.Insert(new Movie("I"));
            collection.Insert(new Movie("B"));
            collection.Insert(new Movie("M"));
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("D"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Adding A, A, A");
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("A"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting B, I, J, B");
            Console.WriteLine(collection.Delete(new Movie("B")));
            Console.WriteLine(collection.Delete(new Movie("I")));
            Console.WriteLine(collection.Delete(new Movie("J")));
            Console.WriteLine(collection.Delete(new Movie("B")));

            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Clearing collection");
            collection.Clear();
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Adding B, A, C");
            collection.Insert(new Movie("B"));
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("C"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting B");
            collection.Delete(new Movie("B"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting A");
            collection.Delete(new Movie("A"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting C");
            collection.Delete(new Movie("C"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Clearing collection");
            collection.Clear();
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Adding B, A, C");
            collection.Insert(new Movie("B"));
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("C"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting A");
            collection.Delete(new Movie("A"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting C");
            collection.Delete(new Movie("C"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Deleting B");
            collection.Delete(new Movie("B"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Adding B, A, C");
            collection.Insert(new Movie("B"));
            collection.Insert(new Movie("A"));
            collection.Insert(new Movie("C"));
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Searching for A");
            Console.WriteLine(collection.Search(new Movie("A")));
            movie = collection.Search("A");
            Console.WriteLine((movie != null) ? movie.ToString() : "null");

            Console.WriteLine("Searching for D");
            Console.WriteLine(collection.Search(new Movie("D")));
            movie = collection.Search("D");
            Console.WriteLine((movie != null) ? movie.ToString() : "null");

            Console.WriteLine("Adding null");
            collection.Insert(null);
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Removing null");
            collection.Delete(null);
            movies = collection.ToArray();
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine(movies[i].ToString());
            }

            Console.WriteLine("Searching null");
            Console.WriteLine(collection.Search((IMovie)null));
            movie = collection.Search((string)null);
            Console.WriteLine((movie != null) ? movie.ToString() : "null");
        }
    }
}
