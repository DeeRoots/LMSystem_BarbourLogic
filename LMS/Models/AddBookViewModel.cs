using LMS.DatabaseModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.Models
{
    public class AddBookViewModel
    {
        public  string Title { get; set; }
        public  string Author { get; set; }
        public  string Publisher { get; set; }
        public  DateTime PublicationDate { get; set; }
        public  string ISBN { get; set; }
        public int BookCategoryId { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public List<SelectListItem> BookCategories = new List<SelectListItem>();


    }
}
