using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JSON_Films {
    internal class Program
    {
        static void Main(string[] args)
        {

            string jsonFilms = File.ReadAllText("Films.json");
            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(jsonFilms);

            films.ForEach(a => Console.WriteLine(a));

            //Add Film to list
            Console.WriteLine("\n\nNew Film Added");

            films.Add(new Film { 
                Film_ID = 88888888, 
                Title = "Hairy Potter and the Potion of Doom", 
                Synopsis = "Hairy, Germione and Don get up to mischief in professor Snipe's potions class", 
                Director = "Pete Behague",
                Release_Date = new DateTime(2022, 09, 10)
            });


            jsonFilms = JsonConvert.SerializeObject(films, Formatting.Indented);
            File.WriteAllText("Films.json", jsonFilms);

            jsonFilms = File.ReadAllText("Films.json");
            films = JsonConvert.DeserializeObject<List<Film>>(jsonFilms);

            films.ForEach(a => Console.WriteLine(a));

            // Using dynamic/anonymous types
            Console.WriteLine("\n\nUsing Dynamic and Anonymous Types");
            dynamic filmObjectsCollection = JsonConvert.DeserializeObject(jsonFilms);

            foreach (dynamic filmObject in filmObjectsCollection)
            {
                Console.WriteLine($"{filmObject.Film_ID}, " +
                    $"{filmObject.Title}, " +
                    $"{filmObject.Synopsis}, " +
                    $"{filmObject.Director} " +
                    $"{filmObject.Release_Date:d}");
            }
        }
    }
}