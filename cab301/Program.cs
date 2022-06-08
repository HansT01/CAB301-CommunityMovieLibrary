using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cab301
{
    class Program
    {
        private static Random r = new Random();
        static void Main(string[] args)
        {
            EmpiricalTests();
            // TestMovieLibraryGUI();
        }

        static void EmpiricalTests()
        {
            // Generate string for movie title
            string GenerateString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghiklmnopqrstuvwxyz0123456789";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[r.Next(s.Length)]).ToArray());
            }

            // Generate movie collection of size k with random movie titles and noborrowings
            MovieCollection GenerateMovies(int k)
            {
                MovieCollection movies = new();
                for (int i = 0; i < k; i++)
                {
                    Movie movie = new(GenerateString(12));
                    movie.NoBorrowings = r.Next();
                    movies.Insert(movie);
                }
                return movies;
            }

            // 10,000 to 200,000 movies in movie array in increments of 10,000
            for (int i = 10000; i <= 200000; i += 10000)
            {
                MovieCollection movies = GenerateMovies(i);

                DateTime startTime = DateTime.Now;
                MovieLibraryGUI.TopMovies(movies.ToArray(), 3);
                DateTime endTime = DateTime.Now;

                TimeSpan totalTime = endTime - startTime;
                Console.WriteLine($"Number of movies: {i}, Time taken (ms): {totalTime.TotalMilliseconds}");
            }
        }

        static void TestMovieLibraryGUI()
        {
            MovieCollection movies = new();
            MemberCollection members = new(100);
            members.Add(new Member("Johnny", "Yang")
            {
                Pin = "1224",
                ContactNumber = "0469999999"
            });
            movies.Insert(new Movie("Action movie", MovieGenre.Action, MovieClassification.M15Plus, 150, 5));
            movies.Insert(new Movie("Comedy movie", MovieGenre.Comedy, MovieClassification.PG, 90, 25));
            movies.Insert(new Movie("Drama movie", MovieGenre.Drama, MovieClassification.PG, 120, 15));
            movies.Insert(new Movie("History movie", MovieGenre.History, MovieClassification.G, 60, 10));
            movies.Insert(new Movie("Another movie", MovieGenre.History, MovieClassification.G, 60, 10));
            movies.Insert(new Movie("Last movie", MovieGenre.History, MovieClassification.G, 60, 10));

            MovieLibraryGUI gui = new MovieLibraryGUI(movies, members);
        }
    }
}
