namespace LMS.DatabaseModels
{
    public class BookReturn
    {  
        public int Id { get; set; }

       
        public Book Book { get; set; }
        public int BookId { get; set; }

        public int BookLeaseId { get; set; }


       
        public ApplicationUser UserLeasing { get; set; }
        public string UserLeasingId { get; set; }        
       
        public DateTime ReturnedOn { get; set; }

        public bool IsOverdue { get; set; }
    }
}
