namespace Physiocure.API.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Fee { get; set; } = string.Empty;
    }
}
