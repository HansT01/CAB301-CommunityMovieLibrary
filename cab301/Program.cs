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
            /*movies.Insert(new Movie("Action movie", MovieGenre.Action, MovieClassification.M15Plus, 150, 5));
            movies.Insert(new Movie("Comedy movie", MovieGenre.Comedy, MovieClassification.PG, 90, 25));
            movies.Insert(new Movie("Drama movie", MovieGenre.Drama, MovieClassification.PG, 120, 15));
            movies.Insert(new Movie("History movie", MovieGenre.History, MovieClassification.G, 60, 10));
            movies.Insert(new Movie("Another movie", MovieGenre.History, MovieClassification.G, 60, 10));
            movies.Insert(new Movie("Last movie", MovieGenre.History, MovieClassification.G, 60, 10));
*/
            MovieLibraryGUI gui = new MovieLibraryGUI(movies, members);

            int[] arr = { 12, 45, 1, -1, 45, 54, 23, 5, 0, -10 };
            Findthree(arr);
        }

        static void Findthree(int[] elements)
        {
            int[] topthree = new int[3];
            for (int j = 0; j < 3; j++)
            {
                int max = elements[j];
                topthree[j] = max;

                for (int i = j + 1; i < elements.Length; i++)
                {
                    if ((elements[i] > max)
                        && (elements[i] != topthree[0])
                        && (elements[i] != topthree[1]))
                    {
                        max = elements[i];
                        topthree[j] = max;
                    }
                }
            }

            foreach (int i in topthree)
            {
                Console.WriteLine(i);
            }
        }
    }
}
