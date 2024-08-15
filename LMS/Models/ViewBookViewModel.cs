using LMS.DatabaseModels;

namespace LMS.Models
{
    public class ViewBookViewModel
    {
        public  string Title { get; set; }
        public  string Author { get; set; }
        public  string Publisher { get; set; }
        public  string PublicationDate { get; set; }
        public string CategoryName { get; set; }
        public  string ISBN { get; set; } 
        public string TotalCopies { get; set; }
        public string AvailableCopies { get; set; }
    }
}
