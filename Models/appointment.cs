using System.ComponentModel.DataAnnotations;

namespace Physiocure.API.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        [Required]
        public string DoctorName { get; set; } = string.Empty;

        public string HospitalName { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;

        // ✅ Better store as DateTime not string
        public DateTime Date { get; set; }

        [Required]
        public string Slot { get; set; } = string.Empty;

        // pending / accepted / rejected
        public string Status { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
