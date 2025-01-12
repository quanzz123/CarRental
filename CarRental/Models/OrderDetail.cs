using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int CarId { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? PickupDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual CarRentalOrder Order { get; set; } = null!;
}
