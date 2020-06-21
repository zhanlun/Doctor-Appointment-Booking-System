using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Models.Patient
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "IC No.")]
        public string ICNo { get; set; }

        [Required]
        [Display(Name = "Phone No.")]
        public string PhoneNo { get; set; }
    }
}
