using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string UserType { get; set; } = null!;

    public string Username { get; set; } = null!;
    public string Sdt { get; set; } = null!;
    public string Address { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
