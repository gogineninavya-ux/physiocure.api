using Microsoft.AspNetCore.Mvc;
using Physiocure.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Physiocure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor
            {
                Id = 1,
                Name = "Dr. Rajesh Kumar",
                Speciality = "Orthopedic",
                HospitalName = "Apollo Hospital",
                WorkingType = "Hospital",
                Address = "M.G Road, Vijayawada",
                District = "Krishna",
                State = "Andhra Pradesh",
                RadiusKm = 10,
                Experience = "10 Years",
                ImageUrl = "https://via.placeholder.com/150",
                Fee = 500
            },
            new Doctor
            {
                Id = 2,
                Name = "Dr. Priya Sharma",
                Speciality = "Physiotherapist",
                HospitalName = "Fortis Hospital",
                WorkingType = "HOospital",
                Address = "Ring Road, Guntur",
                District = "Guntur",
                State = "Andhra Pradesh",
                RadiusKm = 20,
                Experience = "7 Years",
                ImageUrl = "https://via.placeholder.com/150",
                Fee = 400
            }
        };

        // ✅ GET: api/Doctor?district=Krishna&radius=10&workingType=Clinic
        [HttpGet]
        public IActionResult GetDoctors(
            [FromQuery] string? district,
            [FromQuery] int? radius,
            [FromQuery] string? workingType
        )
        {
            var result = doctors.AsQueryable();

            // Andhra Pradesh only
            result = result.Where(d => d.State.ToLower() == "andhra pradesh");

            if (!string.IsNullOrEmpty(district))
            {
                result = result.Where(d => d.District.ToLower() == district.ToLower());
            }

            if (radius.HasValue)
            {
                result = result.Where(d => d.RadiusKm <= radius.Value);
            }

            if (!string.IsNullOrEmpty(workingType))
            {
                result = result.Where(d => d.WorkingType.ToLower() == workingType.ToLower());
            }

            return Ok(result.ToList());
        }

        // GET: api/Doctor/1
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
                return NotFound(new { message = "Doctor not found" });

            return Ok(doctor);
        }

        // POST: api/Doctor
        [HttpPost]
        public IActionResult AddDoctor([FromBody] Doctor doctor)
        {
            if (doctor == null)
                return BadRequest(new { message = "Doctor data is required" });

            doctor.Id = doctors.Count + 1;

            if (string.IsNullOrEmpty(doctor.State))
                doctor.State = "Andhra Pradesh";

            doctors.Add(doctor);

            return Ok(new { message = "Doctor added successfully", doctor });
        }
    }
}
