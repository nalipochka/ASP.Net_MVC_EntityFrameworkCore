namespace ASP.Net_MVC_EntityFrameworkCore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; }= null!;
        public string Genre { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public int YearOfPublish { get; set; }
        public string? FilePath { get; set; } = null!;

        //public byte[]? Photo { get; set; } = null!;
        public string More { get; set; } = null!;

    }
}
