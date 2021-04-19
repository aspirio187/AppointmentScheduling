using AppointmentScheduling.Data;
using AppointmentScheduling.Helpers;
using AppointmentScheduling.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db ??
                throw new ArgumentNullException(nameof(db));
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
    }
}
