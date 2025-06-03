using System;
using System.Threading.Tasks;
using Desafio.Application.Interfaces;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;

namespace Desafio.Application
{
    // Implementação da lógica de negócios relacionada às máquinas
    public class MachineService : IMachineService
    {
        private readonly IGeralPersistence _geralPersistence;           // Interface genérica para operações de banco
        private readonly IMachinePersistence _machinePersistence;       // Interface específica para persistência de máquinas

        public MachineService(IGeralPersistence geralPersistence,
                              IMachinePersistence machinePersistence)
        {
            _geralPersistence = geralPersistence;
            _machinePersistence = machinePersistence;
        }

        /// <summary>
        /// Adiciona uma nova máquina ao banco de dados.
        /// </summary>
        public async Task<Machine> AddMachine(Machine model)
        {
            try
            {
                _geralPersistence.Add<Machine>(model); // Adiciona a máquina ao contexto
                if (await _geralPersistence.SaveChangesAsync()) // Salva as alterações no banco
                {
                    return await _machinePersistence.GetMachineByIdAsync(model.Id); // Retorna a máquina salva
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma máquina existente.
        /// </summary>
        public async Task<Machine> UpdateMachine(Machine model)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(model.Id); // Verifica se a máquina existe
                if (machine == null)
                {
                    return null;
                }

                _geralPersistence.Update(model); // Atualiza os dados
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _machinePersistence.GetMachineByIdAsync(model.Id); // Retorna a máquina atualizada
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma máquina do banco de dados.
        /// </summary>
        public async Task<bool> DeleteMachine(Guid id)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(id);
                if (machine == null)
                {
                    throw new Exception("Máquina para deletar não foi encontrada!");
                }

                _geralPersistence.Delete<Machine>(machine); // Remove a máquina do contexto
                return await _geralPersistence.SaveChangesAsync(); // Salva a exclusão no banco
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todas as máquinas cadastradas.
        /// </summary>
        public async Task<Machine[]> GetAllMachinesAsync()
        {
            try
            {
                var machines = await _machinePersistence.GetAllMachinesAsync();
                if (machines == null) return null;
                return machines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todas as máquinas filtradas por status.
        /// </summary>
        public async Task<Machine[]> GetAllMachinesByStatusAsync(MachineStatus status)
        {
            try
            {
                var machines = await _machinePersistence.GetAllMachinesByStatusAsync(status);
                if (machines == null) return null;
                return machines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma máquina específica pelo ID.
        /// </summary>
        public async Task<Machine> GetMachineByIdAsync(Guid id)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(id);
                if (machine == null) return null;
                return machine;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados da máquina com base nas informações da telemetria.
        /// </summary>
        public async Task<Machine> UpdateMachineTelemetry(Telemetry telemetry)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(telemetry.MachineId);
                if (machine == null)
                {
                    return null;
                }

                // Atualiza os dados da máquina com base na última telemetria
                machine.Location = telemetry.Location;
                machine.Status = telemetry.Status;

                _geralPersistence.Update(machine); // Atualiza no contexto
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _machinePersistence.GetMachineByIdAsync(telemetry.MachineId);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
