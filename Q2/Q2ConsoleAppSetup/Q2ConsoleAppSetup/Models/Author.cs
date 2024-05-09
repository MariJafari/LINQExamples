using System;
using System.Collections.Generic;

namespace Q2ConsoleAppSetup.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Title> Isbns { get; set; } = new List<Title>();
}
