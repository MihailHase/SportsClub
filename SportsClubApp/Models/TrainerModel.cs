using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClubApp.Models
{
    public class TrainerModel
    {
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Trainer Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Details { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
