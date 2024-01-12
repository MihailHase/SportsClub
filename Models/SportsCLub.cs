using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace TrainingStudio02.Models
{
    public class FitnessClass
    {
        public int ID { get; set; }
        [Display(Name = "Name of the class")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public decimal Price { get; set; }
        public int? TrainerID { get; set; }
        public Trainer? Trainer { get; set; }
        public int? LocationID { get; set; }
        public Location? Location { get; set; }
        public ICollection<FitnessClassCategory>? FitnessClassCategories { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
    }
}
