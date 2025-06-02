using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Desafio.Application.Interfaces;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Desafio.API.Hubs;

namespace Desafio.API.Services
{
public class TelemetrySimulatorHostedService : BackgroundService
{
private readonly IServiceProvider _services;
private readonly IHubContext<TelemetryHub> _hub;

    public TelemetrySimulatorHostedService(IServiceProvider services, IHubContext<TelemetryHub> hub)
    {
        _services = services;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var random = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var telemetryService = scope.ServiceProvider.GetRequiredService<ITelemetryService>();
                var machineService = scope.ServiceProvider.GetRequiredService<IMachineService>();

                // 🔍 Recupera todas as máquinas
                var machines = await machineService.GetAllMachinesAsync();

                if (machines != null)
                {
                    foreach (var machine in machines)
                    {
                        // Gera valores simulados
                        var telemetry = new Telemetry
                        {
                            Id = 0,
                            MachineId = machine.Id,
                            Location = $"Lat:{-30 + random.NextDouble() * 10:F4},Lng:{-50 + random.NextDouble() * 10:F4}",
                            Status = (MachineStatus)random.Next(0, 3),
                            Timestamp = DateTime.UtcNow
                        };

                        await telemetryService.AddTelemetry(telemetry);
                        await machineService.UpdateMachineTelemetry(telemetry);

                        // Envia via SignalR
                        await _hub.Clients.All.SendAsync("ReceiveTelemetry", new
                        {
                            id = telemetry.MachineId,
                            status = telemetry.Status.ToString(),
                            location = telemetry.Location
                        });
                    }
                }
            }

            // Aguarda 5 segundos
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
}