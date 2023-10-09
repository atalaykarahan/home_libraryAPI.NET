using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Author
{
    public int Id { get; set; }

    public string AuthorName { get; set; } = null!;

    public string? AuthorSurname { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
