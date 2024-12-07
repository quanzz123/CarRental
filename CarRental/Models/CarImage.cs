using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class CarImage
{
    public int ImageId { get; set; }

    public string? Image1 { get; set; }

    public int? CarId { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }
}
