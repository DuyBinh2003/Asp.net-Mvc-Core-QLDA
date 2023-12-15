using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class Cart
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    //public virtual Invoice Invoice { get; set; } = null!;
}
