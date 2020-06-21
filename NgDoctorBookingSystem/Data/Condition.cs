using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Data
{
    public class Condition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
