using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public string? StatusDescription { get; set; }

    public virtual ICollection<CarRentalOrder> CarRentalOrders { get; set; } = new List<CarRentalOrder>();
}
