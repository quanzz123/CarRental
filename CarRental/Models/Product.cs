using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Title { get; set; }

    public decimal? Price { get; set; }
}
