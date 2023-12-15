using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual User User { get; set; } = null!;

    //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
