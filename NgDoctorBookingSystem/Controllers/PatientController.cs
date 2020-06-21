using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NgDoctorBookingSystem.Data;
using NgDoctorBookingSystem.Models;

namespace NgDoctorBookingSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public PatientController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var model = _appDbContext.Appointments
                .Where(x => x.PatientId == HttpContext.Session.GetInt32(HomeController.SESSION_ID))
                .Include(x => x.Condition)
                .Include(x => x.Doctor)
                .OrderByDescending(x => x.DateApproved)
                .ToList();

            return View(model);
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Book()
        {
            var conditionsList = _appDbContext.Conditions
                .Select(x => new SelectListItem 
                { 
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            var doctorsList = _appDbContext.Doctors
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            var model = new AppointmentViewModel
            {
                ConditionsList = conditionsList,
                DoctorsList = doctorsList,
                DateTime = DateTime.Today.AddDays(7).AddHours(8)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Book(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int patientId = (int)HttpContext.Session.GetInt32(HomeController.SESSION_ID);

            var appointment = new Appointment
            {
                PatientId = patientId,
                ConditionId = model.ConditionId,
                DoctorId = model.DoctorId,
                DateTime = model.DateTime
            };

            _appDbContext.Appointments.Add(appointment);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Patient");
        }
    }
}
