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

        // Injeção de dependência dos serviços utilizados pelo controller
        public MachinesController(ITelemetryService telemetryService, IMachineService machineService)
        {
            _telemetryService = telemetryService;
            _machineService = machineService;
        }

        // GET: api/machines
        // Retorna todas as máquinas cadastradas
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
                // Retorna erro 500 caso ocorra alguma exceção
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Maquinas, Erro: {ex.Message}");
            }
        }

        // GET: api/machines/{id}
        // Retorna uma máquina específica pelo ID
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

        // GET: api/machines/{status}/status
        // Retorna máquinas filtradas por status (conversão de int para enum)
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

        // POST: api/machines
        // Adiciona uma nova máquina
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

        // PUT: api/machines
        // Atualiza os dados de uma máquina existente e adiciona uma nova entrada de telemetria
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Machine machine)
        {
            // Valida se o objeto recebido é válido
            if (machine == null || machine.Id == Guid.Empty)
            {
                return BadRequest("Máquina inválida.");
            }

            try
            {
                // Cria um novo registro de telemetria com os dados recebidos
                var telemetry = new Telemetry
                {
                    Id = 0,
                    MachineId = machine.Id,
                    Location = machine.Location,
                    Status = machine.Status,
                    Timestamp = DateTime.UtcNow // Horário atual em UTC
                }; 

                // Adiciona a telemetria no banco
                var resultInsert = await _telemetryService.AddTelemetry(telemetry);
                if (resultInsert == null) return BadRequest("Erro ao tentar adicionar Telemetria.");

                // Atualiza a telemetria vinculada à máquina
                var resultUpdate = await _machineService.UpdateMachineTelemetry(telemetry);
                if (resultUpdate == null) return BadRequest("Erro ao tentar atualizar a maquina.");

                // Busca a máquina antiga no banco
                var machineOld = await _machineService.GetMachineByIdAsync(machine.Id);
                if (machineOld == null) return NotFound("Nenhuma maquina encontrada.");

                // Atualiza os dados da máquina
                machineOld.Location = machine.Location;
                machineOld.Name = machine.Name;
                machineOld.Status = machine.Status;

                // Salva a máquina atualizada
                var result = await _machineService.UpdateMachine(machineOld);
                if (result == null) return BadRequest("Máquina não encontrada.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Máquina, Erro: {machine}");
            }
        }

        // DELETE: api/machines/{id}
        // Remove uma máquina pelo ID
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
