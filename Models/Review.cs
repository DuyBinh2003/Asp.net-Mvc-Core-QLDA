namespace DoAn.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public string? Content { get; set; }

        public double? Rate { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;

    }
}
