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
    public class MachinesController : ControllerBase
    {
        private readonly DataContext _context;
        public MachinesController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] Machine machine)
        {
            if (machine == null || string.IsNullOrEmpty(machine.Name))
            {
                return BadRequest("Machine data is invalid.");
            }

            machine.Id = Guid.NewGuid();
            _context.Machine.Add(machine);

            // ESSENCIAL: salva as mudanças no banco
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMachineById), new { id = machine.Id }, machine);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Machine);
        }

        [HttpGet("{id}")]
        public IActionResult GetMachineById(Guid id)
        {
            var machine = _context.Machine.Where(machine => machine.Id == id);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }

        [HttpGet("{status}")]
        public IActionResult GetMachineByStatus(string status)
        {
            var machine = _context.Machine.Where(machine => machine.Status == status);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(Guid id, [FromBody] Machine updatedMachine)
        {
            if (updatedMachine == null || string.IsNullOrWhiteSpace(updatedMachine.Name))
            {
                return BadRequest("Invalid machine data.");
            }
            var existingMachine = await _context.Machine.FirstOrDefaultAsync(m => m.Id == id);

            if (existingMachine == null)
            {
                return NotFound();
            }

            // Atualiza os campos
            existingMachine.Name = updatedMachine.Name;
            existingMachine.Location = updatedMachine.Location;
            existingMachine.Status = updatedMachine.Status;

            await _context.SaveChangesAsync();

            return NoContent(); // 204 - sem conteúdo, atualização ok
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMachine(Guid id)
        {
            var machine = _context.Machine.FirstOrDefault(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machine.Remove(machine);
            return NoContent();
        }
    }
}
