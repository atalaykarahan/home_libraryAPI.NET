using System;
using System.Collections.Generic;

namespace home_libraryAPI.Models;

public partial class Authority
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
