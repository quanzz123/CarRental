using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class ContractSettlement
{
    public int SettlementId { get; set; }

    public int ContractId { get; set; }

    public DateOnly? Date { get; set; }

    public string? PaymentMethod { get; set; }

    public int? TotalCarsRented { get; set; }

    public decimal? AdvancePayment { get; set; }

    public decimal? TotalPayment { get; set; }

    public decimal? PaidAmount { get; set; }

    public string? Notes { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual ICollection<ContractSettlementDetail> ContractSettlementDetails { get; set; } = new List<ContractSettlementDetail>();
}
