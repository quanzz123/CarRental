using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int AccountId { get; set; }

    public string Content { get; set; } = null!;

    public string? MessageType { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual User Account { get; set; } = null!;
}
