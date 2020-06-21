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
                        ICNo = "850532106403",
                        PhoneNo = "013018282555"
                    },
                    new Doctor
                    {
                        Name = "Dr Rositah",
                        ICNo = "881315106406",
                        PhoneNo = "013018282555"
                    }
                };

                appDbContext.Doctors.AddRange(doctors);
                appDbContext.SaveChanges();
            }
        }
    }
}