namespace LMS.DatabaseModels
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required DateTime PublicationDate { get; set; }
        public required string ISBN { get; set; }        
        public BookCategory BookCategory { get; set; }
        public int BookCategoryId { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

    }
}
