using AppointmentScheduling.Data;
using AppointmentScheduling.Helpers;
using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public AppointmentService(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db ??
                throw new ArgumentNullException(nameof(db));
            _emailSender = emailSender ??
                throw new ArgumentNullException(nameof(emailSender));
        }

        public List<DoctorViewModel> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(r => r.Name.Equals(Helper.Doctor)) on userRoles.RoleId equals roles.Id
                           select new DoctorViewModel()
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return doctors;
        }

        public List<PatientViewModel> GetPatientList()
        {
            var patients = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(r => r.Name.Equals(Helper.Patient)) on userRoles.RoleId equals roles.Id
                            select new PatientViewModel()
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                          ).ToList();
            return patients;
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            // var startDate = DateTime.Parse(model.startDate) => NE FONCTIONNE PAS IL FAUT UTILISER LA FONCTION SUIVANTE
            var startDate = DateTime.ParseExact(model.StartDate, "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
            //var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration)); => NE FONCTIONNE PAS NON PLUS IL FAUT UTILISER LA FONCTION SUIVANTE
            var endDate = startDate.AddMinutes(Convert.ToDouble(model.Duration));
            var patient = _db.Users.FirstOrDefault(x => x.Id == model.PatientId);
            var doctor = _db.Users.FirstOrDefault(x => x.Id == model.DoctorId);
            if (model is not null && model.Id > 0)
            {
                // update
                return 1;
            }
            else
            {
                // Create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };
                await _emailSender.SendEmailAsync(doctor.Email, "Appointment created",
                    $"Your appointment with {patient.Name} is created and in pending status");
                await _emailSender.SendEmailAsync(patient.Email, "Appointment created",
                    $"Your appointment with {doctor.Name} is created and in pending status");
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<AppointmentViewModel> DoctorsEventsById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        public List<AppointmentViewModel> PatientsEventsById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        public AppointmentViewModel GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault()
            }).SingleOrDefault();
        }

        public async Task<int> Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment is not null)
            {
                _db.Appointments.Remove(appointment);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment is not null)
            {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
