using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class EventType
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
