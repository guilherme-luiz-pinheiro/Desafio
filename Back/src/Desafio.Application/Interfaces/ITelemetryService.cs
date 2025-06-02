using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Application.Interfaces
{
    public interface ITelemetryService
    {
        Task<Telemetry> AddTelemetry(Telemetry model);
        Task<Telemetry> UpdateTelemetry(int id);
        Task<bool> DeleteTelemetry(int id);
        Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status);
        Task<Telemetry[]> GetAllTelemetriesAsync();
        Task<Telemetry> GetTelemetriesByIdAsync(int id);
    }
}