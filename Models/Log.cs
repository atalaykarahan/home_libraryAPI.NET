using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Log
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int EventTypeId { get; set; }

    public DateTime EventDate { get; set; }

    public int? BookId { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? TranslatorId { get; set; }

    public int? PublisherId { get; set; }

    public int? AuthorId { get; set; }

    public int? ReadingId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Category? Category { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public virtual Publisher? Publisher { get; set; }

    public virtual Reading? Reading { get; set; }

    public virtual Translator? Translator { get; set; }

    public virtual User? User { get; set; }
}
