using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Helpers
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Patient = "Patient";
        public static string Doctor = "Doctor";

        public static string AppointmentAdded = "Appointment added successfully.";
        public static string AppointmentUpdated = "Appointment update successfully.";
        public static string AppointmentDeleted = "Appointment deleted successfully.";
        public static string AppointmentExists = "Appointment for selected date and time already exists.";
        public static string AppointmentNotExists = "Appoint do not exists.";

        public static string MeetingConfirm = "Meeting confirm successfully";
        public static string MeetingConfirmError = "Meeting confirm Error";

        public static string AppointmentAddError = "Something went wrong. Please try again.";
        public static string AppointmentUpdateError = "Something went wrong. Please try again.";
        public static string SomethingWentWrong = "Something went wrong. Please try again.";

        public static int Success_Code = 1;
        public static int Failure_Code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Value=Helper.Admin, Text=Helper.Admin},
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Value=Helper.Patient, Text=Helper.Patient},
                    new SelectListItem(){ Value=Helper.Doctor, Text=Helper.Doctor},
                };
            }

        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem() { Value = minute.ToString(), Text = i + "Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem() { Value = minute.ToString(), Text = i + "Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
