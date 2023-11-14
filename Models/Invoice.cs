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

    public virtual InvoiceDetail? InvoiceDetail { get; set; }
}
