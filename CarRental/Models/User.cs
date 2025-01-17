﻿using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
