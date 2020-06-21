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

            if (appDbContext.Conditions.Count() == 0)
            {
                Condition[] conditions =
                {
                    new Condition
                    {
                        Name = "Obsessive-compulsive disorder"
                    },
                    new Condition
                    {
                        Name = "Post-traumatic stress disorder"
                    },
                    new Condition
                    {
                        Name = "Generalised anxiety disorder"
                    },
                    new Condition
                    {
                        Name = "Depression"
                    },
                    new Condition
                    {
                        Name = "Others"
                    }
                };

                appDbContext.Conditions.AddRange(conditions);
                appDbContext.SaveChanges();
            }
        }
    }
}