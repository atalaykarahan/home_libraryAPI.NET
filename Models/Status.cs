using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Status
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
