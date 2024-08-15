namespace LMS.DatabaseModels
{
    public class BookCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Book> Books { get; set; }   
    }
}
