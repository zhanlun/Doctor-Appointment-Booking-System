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
                .ToList();

            return View(model);
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Book()
        {
            return View();
        }
    }
}
