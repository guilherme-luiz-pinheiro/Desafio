using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Persistence.Interfaces
{
    public interface ITelemetryPersistence
    {
        //Telemetries
        Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status);
        Task<Telemetry[]> GetAllTelemetriesAsync();
        Task<Telemetry> GetAllTelemetryByIdAsync(int id);


    }
}