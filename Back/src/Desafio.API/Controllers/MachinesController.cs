using Microsoft.AspNetCore.Mvc;
using System;
using Desafio.Domain;
using System.Threading.Tasks;
using Desafio.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Desafio.Application.Interfaces;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _machineService;
        private readonly ITelemetryService _telemetryService;

        public MachinesController(ITelemetryService telemetryService, IMachineService machineService)
        {
            _telemetryService = telemetryService;

            _machineService = machineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var machines = await _machineService.GetAllMachinesAsync();
                if (machines == null) return NotFound("Nenhuma maquina encontrada.");
                return Ok(machines);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Maquinas, Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var machine = await _machineService.GetMachineByIdAsync(id);
                if (machine == null) return NotFound("Nenhuma maquina encontrada.");
                return Ok(machine);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Maquinas, Erro: {ex.Message}");
            }
        }

        [HttpGet("{status}/status")]
        public async Task<IActionResult> GetByStatus(int status)
        {
            try
            {
                var machineStatus = (MachineStatus)status;
                var machine = await _machineService.GetAllMachinesByStatusAsync(machineStatus);
                if (machine == null) return NotFound("Nenhuma maquina encontrada.");
                return Ok(machine);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Maquinas, Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Machine machine)
        {
            try
            {
                var result = await _machineService.AddMachine(machine);
                if (result == null) return BadRequest("Erro ao tentar adicionar Maquina.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar Maquina, Erro: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Machine machine)
        {
            if (machine == null || machine.Id == Guid.Empty)
            {
                return BadRequest("Máquina inválida.");
            }
            try
            {
                var telemetry = new Telemetry
                {
                    Id = 0,
                    MachineId = machine.Id,
                    Location = machine.Location,
                    Status = machine.Status,
                    Timestamp = DateTime.UtcNow
                }; 

                var resultInsert = await _telemetryService.AddTelemetry(telemetry);
                if (resultInsert == null) return BadRequest("Erro ao tentar adicionar Telemetria.");
                var resultUpdate = await _machineService.UpdateMachineTelemetry(telemetry);
                if (resultUpdate == null) return BadRequest("Erro ao tentar atualizar a maquina.");

                var machineOld = await _machineService.GetMachineByIdAsync(machine.Id);
                if (machineOld == null) return NotFound("Nenhuma maquina encontrada.");
                machineOld.Location = machine.Location;
                machineOld.Name = machine.Name;
                machineOld.Status = machine.Status;

                var result = await _machineService.UpdateMachine(machineOld);
                if (result == null) return BadRequest("Máquina não encontrada.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Máquina, Erro: {machine}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _machineService.DeleteMachine(id);
                if (!result) return BadRequest("Maquina não encontrada.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar Maquina, Erro: {ex.Message}");
            }
        }
    }
}
