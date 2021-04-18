using System;

namespace AppointmentScheduling.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        // TODO : Ajouter des merdes
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
