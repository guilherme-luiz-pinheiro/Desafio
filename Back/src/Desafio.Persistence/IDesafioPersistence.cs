using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Persistence
{
    public interface IDesafioPersistence
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

        //MACHINE
        Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status);
        Task<Machine[]> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(Guid id);

        //Telemetries
        Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status);
        Task<Telemetry[]> GetAllTelemetriesAsync();
        Task<Telemetry> GetAllTelemetryByIdAsync(int id);


    }
}