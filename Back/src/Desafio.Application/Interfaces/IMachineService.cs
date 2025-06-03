using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Application.Interfaces
{
    // Interface que define os contratos do serviço de máquinas
    public interface IMachineService
    {
        Task<Machine> AddMachine(Machine model);
        Task<Machine> UpdateMachine(Machine model);
        Task<Machine> UpdateMachineTelemetry(Telemetry telemetry);
        Task<bool> DeleteMachine(Guid id);
        Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status);
        Task<Machine[]> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(Guid id);

    }

}