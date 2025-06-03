using System;
using System.Threading.Tasks;
using Desafio.Domain;
using Desafio.Domain.Enums;

namespace Desafio.Persistence.Interfaces
{
    public interface IMachinePersistence
    {
        // Retorna todas as máquinas filtradas pelo status especificado
        Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status);

        // Retorna todas as máquinas cadastradas
        Task<Machine[]> GetAllMachinesAsync();

        // Retorna uma máquina específica pelo seu identificador único (GUID)
        Task<Machine> GetMachineByIdAsync(Guid id);
    }
}
