using System.ComponentModel.DataAnnotations;

namespace SportsCLubClasses.Models
{
    public class Subscription
    {
        public int ID { get; set; }
        public int? MemberID { get; set; }
        public Member? Member { get; set; }
        public int? SportsCLubID { get; set; }
        public SportsCLub? SportsCLub { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
    }
}
