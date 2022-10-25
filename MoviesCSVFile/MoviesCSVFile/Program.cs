using System;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;


namespace csvFile_lab
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using (var sr = new StreamReader("streaming_movies.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var movies = reader.GetRecords<Movie>().Where(m => m.StreamedOn.Equals("Netflix")).ToList();
                Console.WriteLine("Netflix only:");
                foreach (Movie m in movies)
                {
                    Console.WriteLine($"Title: {m.Title}, Release Year: {m.ReleaseYear}, Genres: {m.Genres}, Revenue: {m.Revenue}");
                }
            }

            using (var sr = new StreamReader("streaming_movies.csv"))
            using (var wr = new StreamWriter("movies_on_netflix.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                using (var writer = new CsvWriter(wr, CultureInfo.InvariantCulture))
                {
                    var movies = reader.GetRecords<Movie>().Where(m => m.StreamedOn == "Netflix").OrderBy(m => m.Title).Select(m => new { Name = m.Title, Year = m.ReleaseYear, Revenue = m.Revenue}).ToList();
                    Console.WriteLine("Writing Netflix only to file:");
                    writer.WriteRecords(movies);
                }
            }
        }
    }
}
