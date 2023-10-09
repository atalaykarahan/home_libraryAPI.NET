using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Book
{
    public int Id { get; set; }

    public string BookTitle { get; set; } = null!;

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int StatusId { get; set; }

    public string? ImagePath { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Translator> Translators { get; set; } = new List<Translator>();
}
