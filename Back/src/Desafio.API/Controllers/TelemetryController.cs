using Microsoft.AspNetCore.Mvc;
using Desafio.Domain;
using System.Threading.Tasks;
using Desafio.Domain.Enums;
using Desafio.Application.Interfaces;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Desafio.API.Hubs;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly IHubContext<TelemetryHub> _hub;

        private readonly ITelemetryService _telemetryService;
        private readonly IMachineService _machineService;

        public TelemetryController(ITelemetryService telemetryService, IMachineService machineService, IHubContext<TelemetryHub> hub)
        {
            _telemetryService = telemetryService;
            _machineService = machineService;
            _hub = hub;
        }

        [HttpGet]
        public async Task<ActionResult<Telemetry[]>> GetAll()
        {
            try
            {
                var telemetries = await _telemetryService.GetAllTelemetriesAsync();
                if (telemetries == null) return NotFound("Nenhuma telemetria encontrada.");
                return Ok(telemetries);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Telemetrias, Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Telemetry telemetry)
        {
            try
            {
                var resultInsert = await _telemetryService.AddTelemetry(telemetry);
                if (resultInsert == null) return BadRequest("Erro ao tentar adicionar Telemetria.");
                var resultUpdate = await _machineService.UpdateMachineTelemetry(telemetry);
                if (resultUpdate == null) return BadRequest("Erro ao tentar atualizar a maquina.");
                await _hub.Clients.All.SendAsync("ReceiveTelemetry", new
                {
                    id = telemetry.MachineId,
                    status = telemetry.Status,
                    location = telemetry.Location
                });
                
                return Ok(resultInsert);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Telemetria, Erro: {ex.Message}");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<Telemetry[]>> GetByStatus(MachineStatus status)
        {
            try
            {
                var telemetry = await _telemetryService.GetAllTelemetriesByStatusAsync(status);
                if (telemetry == null) return NotFound("Nenhuma Telemetria encontrada.");
                return Ok(telemetry);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Telemetrias, Erro: {ex.Message}");
            }
        }
    }

}
