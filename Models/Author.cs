using System;
using System.Collections.Generic;

namespace DoAn.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? Name { get; set; }

    public string? ImgPath { get; set; }

    public string? Description { get; set; }
}
