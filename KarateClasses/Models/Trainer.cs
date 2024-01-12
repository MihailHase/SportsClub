using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SportsCLubClasses.Models
{
    public class Trainer
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Trainer Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string? Details { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ICollection<SportsCLub>? SportsCLub { get; set; }
    }
}
