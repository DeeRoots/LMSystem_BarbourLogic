namespace LMS.Models
{
    public class LeasedBookViewModel
    {
        public string BookLeaseId { get; set; }
        public string BookName { get; set; }
        public string UserLeasedName { get; set; }
        public string LeasedOn {  get; set; }
        public string ReturnBy { get; set; }
        public bool Overdue { get; set; }

    }
}
