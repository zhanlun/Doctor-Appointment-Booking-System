using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Data
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? DateApproved { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
