using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Physiocure.API.Data;
using Physiocure.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Physiocure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return Ok(appointments);
        }

        // GET: api/Appointments/user/1
        [HttpGet("user/{clientId}")]
        public async Task<IActionResult> GetAppointmentsByUser(int clientId)
        {
            var list = await _context.Appointments
                .Where(a => a.ClientId == clientId)
                .ToListAsync();

            return Ok(list);
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment appointment)
        {
            appointment.Status = "pending";
            appointment.CreatedAt = DateTime.Now;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment booked successfully", appointment });
        }

        // PUT: api/Appointments/5/accept
        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptAppointment(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);

            if (appt == null)
                return NotFound(new { message = "Appointment not found" });

            appt.Status = "accepted";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment accepted" });
        }

        // PUT: api/Appointments/5/reject
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectAppointment(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);

            if (appt == null)
                return NotFound(new { message = "Appointment not found" });

            appt.Status = "rejected";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment rejected" });
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);

            if (appt == null)
                return NotFound(new { message = "Appointment not found" });

            _context.Appointments.Remove(appt);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment deleted" });
        }
    }
}
