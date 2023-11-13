using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ImgPath { get; set; }

    public string? Description { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public string? BookPath { get; set; }

    public string? Isbn { get; set; }

    public string? Status { get; set; }
}
