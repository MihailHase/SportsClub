using System.ComponentModel.DataAnnotations;

namespace TrainingStudio02.Models
{
    public class Subscription
    {
        public int ID { get; set; }
        public int? MemberID { get; set; }
        public Member? Member { get; set; }
        public int? FitnessClassID { get; set; }
        public FitnessClass? FitnessClass { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
    }
}
