// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Diagnostics.Metrics;

Console.WriteLine("Question 01 - Lab 04");

// Invokes methods
Question1_1();
Question1_2();
Question1_3();
Question1_4();
Question1_5();
Question1_6();


// 1.1 List the names of the countries in alphabetical order [0.5 mark]
void Question1_1()
{
    Console.WriteLine("\n/////////////////////Question1_1////////////////////");

    var countries = Country.GetCountries();
    var sortedCountries = countries.OrderBy(c => c.Name);
    Console.WriteLine("\nCountries in alphabetical order:");
    foreach (var country in sortedCountries)
    {
        Console.WriteLine(country.Name);
    }
}

// 1.2 List the names of the countries in descending order of number of resources [0.5 mark]
void Question1_2()
{
    Console.WriteLine("\n/////////////////////Question1_2////////////////////");

    var countries = Country.GetCountries();
    var sortedCountries = countries.OrderByDescending(c => c.Resources.Count);
    Console.WriteLine("\nCountries in descending order of number of resources:");
    foreach (var country in sortedCountries)
    {
        Console.WriteLine($"{country.Name} - {country.Resources.Count} resources");
    }

}

// 1.3 List the names of the countries that shares a border with Argentina [0.5 mark]
void Question1_3()
{
    Console.WriteLine("\n/////////////////////Question1_3////////////////////");

    var countries = Country.GetCountries();
    var neighboringCountries = countries.Where(c => c.Borders.Contains("Argentina"));
    Console.WriteLine("\nCountries that share a border with Argentina:");
    foreach (var country in neighboringCountries)
    {
        Console.WriteLine(country.Name);
    }

}

// 1.4 List the names of the countries that has more than 10,000,000 population [0.5 mark]
void Question1_4()
{
    Console.WriteLine("\n/////////////////////Question1_4////////////////////");

    var countries = Country.GetCountries();
    var populousCountries = countries.Where(c => c.Population > 10000000);
    Console.WriteLine("\nCountries with more than 10,000,000 population:");
    foreach (var country in populousCountries)
    {
        Console.WriteLine($"{country.Name} - Population: {country.Population}m");
    }

}

// 1.5 List the country with highest population [1 mark]
void Question1_5()
{
    Console.WriteLine("\n/////////////////////Question1_5////////////////////");

    var countries = Country.GetCountries();
    var countryWithMaxPopulation = countries.OrderByDescending(c => c.Population).First();
    Console.WriteLine($"\nCountry with the highest population: {countryWithMaxPopulation.Name} ({countryWithMaxPopulation.Population}m)");

}

// 1.6 List all the religion in south America in dictionary order [1 mark]
void Question1_6()
{
    Console.WriteLine("\n/////////////////////Question1_6////////////////////");

    var countries = Country.GetCountries();
    var religions = countries.SelectMany(c => c.Religions).Distinct().OrderBy(r => r);
    Console.WriteLine("\nReligions in South America in dictionary order:");
    foreach (var religion in religions)
    {
        Console.WriteLine(religion);
    }

}
