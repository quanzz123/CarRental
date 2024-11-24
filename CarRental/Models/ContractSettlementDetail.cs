using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class ContractSettlementDetail
{
    public int SettlementDetailId { get; set; }

    public int SettlementId { get; set; }

    public int CarId { get; set; }

    public DateOnly? ReceiveDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public virtual ContractSettlement Settlement { get; set; } = null!;
}
