// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Q2ConsoleAppSetup.Models;

Console.WriteLine("Question 02 - Lab 04");


// Invokes methods
Question2_1();
Question2_2();
Question2_3();



//1.Get a list of all the titles and the authors who wrote them. Sort the results by title. [2 marks]
void Question2_1()
{
    Console.WriteLine("\n/////////////////////Question2_1////////////////////\n");

    using (var dbContext = new BooksDBContext())
    {
        var query = from title in dbContext.Titles
                    orderby title.Title1
                    select new
                    {
                        Title = title.Title1,
                        Authors = title.Authors.Select(author => author.FirstName + " " + author.LastName)
                    };

        Console.WriteLine("list of all the titles and the authors:\n");
        foreach (var item in query)
        {
            Console.WriteLine($"Title: {item.Title}");
            Console.WriteLine($"Authors: {string.Join(", ", item.Authors)}");
            Console.WriteLine();
        }
    }

}

//2.Get a list of all the titles and the authors who wrote them. Sort the results by title.  Each title sort the authors alphabetically by last name, then first name[4 marks]
void Question2_2()
{
    Console.WriteLine("\n/////////////////////Question2_2////////////////////\n");

    using (var dbContext = new BooksDBContext())
    {
        var query = dbContext.Titles.OrderBy(t => t.Title1)
                    .Select(t => new
                    {
                        Title = t.Title1,
                        Authors = t.Authors
                                   .OrderBy(a => a.LastName)
                                   .ThenBy(a => a.FirstName)
                                   .Select(a => a.FirstName + " " + a.LastName)
                    });

        Console.WriteLine("List of all the titles and the authors sorted alphabetically:\n");
        foreach (var item in query)
        {
            Console.WriteLine($"Title: {item.Title}");
            Console.WriteLine($"Authors: {string.Join(", ", item.Authors)}");
            Console.WriteLine();
        }
    }
}

//3.Get a list of all the authors grouped by title, sorted by title; for a given title sort the author names alphabetically by last name then first name.[4 marks]
void Question2_3()
{
    Console.WriteLine("\n/////////////////////Question2_3////////////////////\n");
    using (var dbContext = new BooksDBContext())
    {
        // First, let's get all authors with their titles, then group by title.
        var authorsWithTitle = dbContext.Authors
                                        .SelectMany(author => author.Isbns, (author, title) => new { author, title })
                                        .OrderBy(at => at.title.Title1)
                                        .ThenBy(at => at.author.LastName)
                                        .ThenBy(at => at.author.FirstName)
                                        .ToList();

        var groupedByTitle = authorsWithTitle
                             .GroupBy(at => at.title.Title1)
                             .Select(group => new
                             {
                                 Title = group.Key,
                                 Authors = group.Select(g => $"{g.author.FirstName} {g.author.LastName}").ToList()
                             })
                             .OrderBy(g => g.Title);

        Console.WriteLine("List of all the authors grouped by title, sorted by title:\n");
        foreach (var group in groupedByTitle)
        {
            Console.WriteLine($"Title: {group.Title}");
            Console.WriteLine($"Authors: {string.Join(", ", group.Authors)}");
            Console.WriteLine();
        }
    }
}
