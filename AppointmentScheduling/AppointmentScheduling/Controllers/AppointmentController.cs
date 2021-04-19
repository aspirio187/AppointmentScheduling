﻿using AppointmentScheduling.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ??
                throw new ArgumentNullException(nameof(appointmentService));
        }

        public IActionResult Index()
        {
            _appointmentService.GetDoctorList();
            return View();
        }
    }
}