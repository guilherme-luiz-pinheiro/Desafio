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
    [Route("api/[controller]")] // Rota base: api/telemetry
    public class TelemetryController : ControllerBase
    {
        private readonly IHubContext<TelemetryHub> _hub; // Hub para comunicação em tempo real via SignalR
        private readonly ITelemetryService _telemetryService;
        private readonly IMachineService _machineService;

        // Construtor com injeção de dependência dos serviços
        public TelemetryController(ITelemetryService telemetryService, IMachineService machineService, IHubContext<TelemetryHub> hub)
        {
            _telemetryService = telemetryService;
            _machineService = machineService;
            _hub = hub;
        }

        // GET: api/telemetry
        // Retorna todas as telemetrias registradas
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
                // Retorna erro 500 caso ocorra falha na requisição
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Telemetrias, Erro: {ex.Message}");
            }
        }

        // POST: api/telemetry
        // Adiciona uma nova telemetria e atualiza os dados da máquina correspondente
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Telemetry telemetry)
        {
            try
            {
                // Insere a telemetria no banco de dados
                var resultInsert = await _telemetryService.AddTelemetry(telemetry);
                if (resultInsert == null) return BadRequest("Erro ao tentar adicionar Telemetria.");

                // Atualiza os dados da máquina com as informações da telemetria
                var resultUpdate = await _machineService.UpdateMachineTelemetry(telemetry);
                if (resultUpdate == null) return BadRequest("Erro ao tentar atualizar a maquina.");

                // Envia os dados da telemetria para todos os clientes conectados via SignalR
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

        // GET: api/telemetry/status/{status}
        // Retorna todas as telemetrias com um determinado status (ativo, inativo, manutenção etc.)
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
