using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Persistence.Interfaces
{
    public interface IMachinePersistence
    {
        Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status);
        Task<Machine[]> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(Guid id);

    }
}