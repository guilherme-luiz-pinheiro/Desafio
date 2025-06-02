using System;
using System.Threading.Tasks;
using Desafio.Application.Interfaces;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;

namespace Desafio.Application
{
    public class MachineService : IMachineService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IMachinePersistence _machinePersistence;

        public MachineService(IGeralPersistence geralPersistence,
                              IMachinePersistence machinePersistence)
        {
            _geralPersistence = geralPersistence;
            _machinePersistence = machinePersistence;
        }

        public async Task<Machine> AddMachine(Machine model)
        {
            try
            {
                _geralPersistence.Add<Machine>(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _machinePersistence.GetMachineByIdAsync(model.Id);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Machine> UpdateMachine(Machine model)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(model.Id);
                if (machine == null)
                {
                    return null;
                }
                _geralPersistence.Update(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _machinePersistence.GetMachineByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMachine(Guid id)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(id);
                if (machine == null)
                {
                    throw new Exception("Maquina para delete não foi encontrado!!");
                }
                _geralPersistence.Delete<Machine>(machine);

                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public async Task<Machine> GetMachineByIdAsync(Guid id)
        {
            try
            {
                var machines = await _machinePersistence.GetMachineByIdAsync(id);
                if (machines == null) return null;
                return machines;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Machine> UpdateMachineTelemetry(Telemetry telemetry)
        {
            try
            {
                var machine = await _machinePersistence.GetMachineByIdAsync(telemetry.MachineId);
                if (machine == null)
                {
                    return null;
                }
                machine.Location = telemetry.Location;
                machine.Status = telemetry.Status;

                _geralPersistence.Update(machine);
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