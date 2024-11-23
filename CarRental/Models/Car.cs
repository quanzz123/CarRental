using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public partial class Car
{
    public int CarId { get; set; }
    [Required(ErrorMessage = "Please select a valid Car Type.")]
    public int CarTypeId { get; set; }

    public string? CarName { get; set; }

    public string? LicensePlate { get; set; }

    public decimal? Price { get; set; }

    public int? DaysBooked { get; set; }

    public decimal? Deposit { get; set; }

    public string? Condition { get; set; }

    public string? Description { get; set; }

    public string? Color { get; set; }

    public int? Model { get; set; }

    public double? Rate { get; set; }

    public string? CarBrand { get; set; }

    public string? Image { get; set; }

    public decimal? SalePrice { get; set; }

    public string? Alias { get; set; }

    public virtual CarType CarType { get; set; } = null!;

    public virtual ICollection<ContractSettlementDetail> ContractSettlementDetails { get; set; } = new List<ContractSettlementDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
