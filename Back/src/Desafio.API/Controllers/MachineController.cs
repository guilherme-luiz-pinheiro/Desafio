﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.API.Models;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/machines")]
    public class MachineController : ControllerBase
    {
        private static List<Machine> machines = new List<Machine>();

        [HttpPost]
        public IActionResult CreateMachine([FromBody] Machine machine)
        {
            if (machine == null || string.IsNullOrEmpty(machine.Name))
            {
                return BadRequest("Machine data is invalid.");
            }

            machine.Id = Guid.NewGuid();
            machines.Add(machine);
            return CreatedAtAction(nameof(GetMachineById), new { id = machine.Id }, machine);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(machines);
        }

        [HttpGet("{id}")]
        public IActionResult GetMachineById(Guid id)
        {
            var machine = machines.FirstOrDefault(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMachine(Guid id, [FromBody] Machine updatedMachine)
        {
            if (updatedMachine == null || string.IsNullOrEmpty(updatedMachine.Name))
            {
                return BadRequest("Invalid machine data.");
            }

            var existingMachine = machines.FirstOrDefault(m => m.Id == id);
            if (existingMachine == null)
            {
                return NotFound();
            }

            // Atualiza os campos
            existingMachine.Name = updatedMachine.Name;
            existingMachine.Location = updatedMachine.Location;
            existingMachine.Status = updatedMachine.Status;

            return NoContent(); // 204 - sem conteúdo, atualização ok
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMachine(Guid id)
        {
            var machine = machines.FirstOrDefault(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            machines.Remove(machine);
            return NoContent();
        }
    }
}
