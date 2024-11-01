using System;
using System.Collections.Generic;

namespace CarRental.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public int? AccountId { get; set; }

    public bool IsActive { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();
}
