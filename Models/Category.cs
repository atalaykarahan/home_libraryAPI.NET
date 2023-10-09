using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
