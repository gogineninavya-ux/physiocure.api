using System.ComponentModel.DataAnnotations.Schema;

namespace Physiocure.API.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;

        public string WorkingType { get; set; } = string.Empty; // Clinic/Hospital/Own Hospital
        public string Address { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string State { get; set; } = "Andhra Pradesh";

        public int RadiusKm { get; set; } // 10,20,30

        public string Experience { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        
    [Column(TypeName = "decimal(18,2)")]
    public decimal Fee { get; set; }
    }
}
