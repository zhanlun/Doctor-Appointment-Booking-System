using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Data
{
    public class AppDbInit
    {
        public static void SeedData(AppDbContext appDbContext)
        {
            if (appDbContext.Doctors.Count() == 0)
            {
                Doctor[] doctors =
                {
                    new Doctor
                    {
                        Name = "Dr Hafiz",
                        ICNo = "888888888888",
                        PhoneNo = "0123456789"
                    },
                    new Doctor
                    {
                        Name = "Dr Rositah",
                        ICNo = "999999999999",
                        PhoneNo = "0198765432"
                    }
                };

                appDbContext.Doctors.AddRange(doctors);
                appDbContext.SaveChanges();
            }
        }
    }
}