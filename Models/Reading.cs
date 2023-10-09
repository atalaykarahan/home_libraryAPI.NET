using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Reading
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public int StatusId { get; set; }

    public string? Comment { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
