using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.API.Models;
using Desafio.API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly DataContext _context;

        public TelemetryController(DataContext context)
        {
            _context = context;
        }

        // POST: api/Telemetry
        [HttpPost]
        public async Task<IActionResult> PostTelemetry([FromBody] Telemetry telemetry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            telemetry.Timestamp = DateTime.UtcNow;

            _context.Telemetry.Add(telemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelemetryById), new { id = telemetry.Id }, telemetry);
        }

        // GET: api/Telemetry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telemetry>> GetTelemetryById(int id)
        {
            var telemetry = await _context.Telemetry.FindAsync(id);

            if (telemetry == null)
                return NotFound();

            return telemetry;
        }

        // GET: api/Telemetry/machine/7c748fc3-xxxx-yyyy-zzzz-abc123
        [HttpGet("machine/{machineId}")]
        public async Task<ActionResult> GetTelemetryByMachineId(Guid machineId)
        {
            var telemetries = await _context.Telemetry
                .Where(t => t.MachineId == machineId)
                .OrderByDescending(t => t.Timestamp)
                .ToListAsync();

            return Ok(telemetries);
        }
    }
}
