using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int AuthorityId { get; set; }

    public virtual Authority Authority { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
