using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NgDoctorBookingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgDoctorBookingSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly AppDbContext _appDbContext;

        public DoctorController(ILogger<DoctorController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var model = _appDbContext.Appointments
                .Where(x => x.DoctorId == HttpContext.Session.GetInt32(HomeController.SESSION_ID))
                .Include(x => x.Condition)
                .Include(x => x.Patient)
                .ToList();

            return View(model);
        }

        public IActionResult Approve(int id)
        {
            int doctorId = (int)HttpContext.Session.GetInt32(HomeController.SESSION_ID);

            var appointment = _appDbContext.Appointments
                .Where(x => x.DoctorId == doctorId)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (appointment == null)
            {
                return RedirectToAction("Index", "Doctor");
            }

            appointment.DateApproved = DateTime.Now;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Doctor");
        }
    }
}
