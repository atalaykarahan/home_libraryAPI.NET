using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
