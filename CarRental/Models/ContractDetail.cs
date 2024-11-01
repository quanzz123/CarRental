using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class ContractDetail
{
    public int ContractDetailId { get; set; }

    public int ContractId { get; set; }

    public int CarTypeId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? ReceiveDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Notes { get; set; }

    public virtual CarType CarType { get; set; } = null!;

    public virtual Contract Contract { get; set; } = null!;
}
