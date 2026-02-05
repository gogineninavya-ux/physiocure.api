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
                Experience = "10 Years",
                ImageUrl = "https://via.placeholder.com/150",
                Fee = "500"
            },
            new Doctor
            {
                Id = 2,
                Name = "Dr. Priya Sharma",
                Speciality = "Physiotherapist",
                HospitalName = "Fortis Hospital",
                Experience = "7 Years",
                ImageUrl = "https://via.placeholder.com/150",
                Fee = "400"
            }
        };

        // GET: api/Doctor
        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(doctors);
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
            doctors.Add(doctor);

            return Ok(new { message = "Doctor added successfully", doctor });
        }
    }
}
