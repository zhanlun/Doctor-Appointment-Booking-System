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
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            int patientId = (int)HttpContext.Session.GetInt32(HomeController.SESSION_ID);

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

            var model = _appDbContext.Appointments
                .Where(x => x.PatientId == patientId)
                .Where(x => x.Id == id)
                .Select(x => new AppointmentViewModel
                {
                    Id = x.Id,
                    ConditionId = x.ConditionId,
                    DoctorId = x.DoctorId,
                    DateTime = x.DateTime,
                    ConditionsList = conditionsList,
                    DoctorsList = doctorsList
                })
                .FirstOrDefault();
            
            if (model == null)
            {
                return RedirectToAction("Index", "Patient");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int patientId = (int)HttpContext.Session.GetInt32(HomeController.SESSION_ID);

            var appointment = _appDbContext.Appointments
                .Where(x => x.PatientId == patientId)
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            if (appointment == null)
            {
                return RedirectToAction("Index", "Patient");
            }

            appointment.DoctorId = model.DoctorId;
            appointment.ConditionId = model.ConditionId;
            appointment.DateTime = model.DateTime;

            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Patient");
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

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            int patientId = (int)HttpContext.Session.GetInt32(HomeController.SESSION_ID);

            var appointment = _appDbContext.Appointments
                .Where(x => x.PatientId == patientId)
                .Where(x => x.Id ==id)
                .FirstOrDefault();

            if (appointment == null)
            {
                return RedirectToAction("Index", "Patient");
            }

            _appDbContext.Appointments.Remove(appointment);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Patient");
        }
    }
}
