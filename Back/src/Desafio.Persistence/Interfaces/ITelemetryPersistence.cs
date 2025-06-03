using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Persistence.Interfaces
{
    public interface ITelemetryPersistence
    {
        // Retorna todas as telemetrias filtradas pelo status da máquina
        Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status);

        // Retorna todas as telemetrias cadastradas
        Task<Telemetry[]> GetAllTelemetriesAsync();

        // Retorna uma telemetria específica pelo seu identificador (int)
        Task<Telemetry> GetAllTelemetryByIdAsync(int id);
    }
}
