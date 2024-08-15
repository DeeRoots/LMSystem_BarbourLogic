namespace LMS.DatabaseModels
{
    public class BookLease
    {
        public int Id { get; set; }
               
        public Book Book { get; set; }
        public int BookId { get; set; }


        public ApplicationUser UserLeasing { get; set; }
        public string UserLeasingId { get; set; }


        public DateTime LeasedOn { get; set; }

        public DateTime ReturnBy { get; set; }

        public bool Returned { get; set; }

    }
}
