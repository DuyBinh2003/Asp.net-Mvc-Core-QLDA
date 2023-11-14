using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class InvoiceDetail
{
    public int InvoiceId { get; set; }

    public int BookId { get; set; }

    public decimal? UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Invoice Invoice { get; set; } = null!;
}
