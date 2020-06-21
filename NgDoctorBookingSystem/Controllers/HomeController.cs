using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NgDoctorBookingSystem.Data;
using NgDoctorBookingSystem.Models;
using NgDoctorBookingSystem.Models.Patient;

namespace NgDoctorBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public const string SESSION_ROLE = "_ROLE";
        public const string SESSION_ID = "_ID";

        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            string role = model.Role;
            int id = -1;

            if (role == "patient")
            {
                var user = _appDbContext.Patients
                    .Where(x => x.ICNo == model.ICNo && x.PhoneNo == model.PhoneNo)
                    .FirstOrDefault();

                if (user == null)
                {
                    ViewData["msg"] = "Not found!";
                    return View();
                }

                id = user.Id;
            }
            else if (role == "doctor")
            {
                var user = _appDbContext.Doctors
                    .Where(x => x.ICNo == model.ICNo && x.PhoneNo == model.PhoneNo)
                    .FirstOrDefault();

                if (user == null)
                {
                    ViewData["msg"] = "Not found!";
                    return View();
                }

                id = user.Id;
            }
            else
            {
                return View();
            }

            HttpContext.Session.SetString(SESSION_ROLE, role);
            HttpContext.Session.SetInt32(SESSION_ID, id);

            return RedirectToAction("Index", role);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_appDbContext.Patients.Where(x => x.ICNo == model.ICNo).Count() > 0)
            {
                ViewData["msg"] = "IC No is in use!";
                return View(model);
            }

            var patient = new Patient
            {
                ICNo = model.ICNo,
                Name = model.Name,
                PhoneNo = model.PhoneNo,
                RegisteredDate = DateTime.Now
            };

            _appDbContext.Patients.Add(patient);
            _appDbContext.SaveChanges();

            // login
            int id = patient.Id;

            HttpContext.Session.SetString(SESSION_ROLE, "patient");
            HttpContext.Session.SetInt32(SESSION_ID, id);

            return RedirectToAction("Index", "Patient");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
