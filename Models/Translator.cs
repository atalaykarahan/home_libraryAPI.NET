using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Translator
{
    public int Id { get; set; }

    public string TranslatorName { get; set; } = null!;

    public string? TranslatorSurname { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
