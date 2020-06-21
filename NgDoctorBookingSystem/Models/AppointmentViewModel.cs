using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Models
{
    public class AppointmentViewModel : Data.Appointment
    {
        public IEnumerable<SelectListItem> Conditions { get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; }
    }
}
