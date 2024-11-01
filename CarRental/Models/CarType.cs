using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class CarType
{
    public int CarTypeId { get; set; }

    public string? CarTypeName { get; set; }

    public int? Seats { get; set; }

    public int? Quantity { get; set; }

    public string? Manufacturer { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<ContractDetail> ContractDetails { get; set; } = new List<ContractDetail>();
}
