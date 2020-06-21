﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ICNo { get; set; }
        public string PhoneNo { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
