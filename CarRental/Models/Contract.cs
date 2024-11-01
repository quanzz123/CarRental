using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Content { get; set; }

    public string? GeneralTerms { get; set; }

    public string? SpecificTerms { get; set; }

    public decimal? AdvancePayment { get; set; }

    public virtual ICollection<ContractDetail> ContractDetails { get; set; } = new List<ContractDetail>();

    public virtual ICollection<ContractSettlement> ContractSettlements { get; set; } = new List<ContractSettlement>();

    public virtual Customer Customer { get; set; } = null!;
}
