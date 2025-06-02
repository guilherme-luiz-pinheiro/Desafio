using System;
using System.Threading.Tasks;
using Desafio.Application.Interfaces;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;

namespace Desafio.Application
{
    public class TelemetryService : ITelemetryService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly ITelemetryPersistence _telemetryPersistence;

        public TelemetryService(IGeralPersistence geralPersistence,
                              ITelemetryPersistence machinePersistence)
        {
            _geralPersistence = geralPersistence;
            _telemetryPersistence = machinePersistence;
        }

        public async Task<Telemetry> AddTelemetry(Telemetry model)
        {
            try
            {
                _geralPersistence.Add<Telemetry>(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _telemetryPersistence.GetAllTelemetryByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Telemetry> UpdateTelemetry(int id)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                if (telemetry == null)
                {
                    return null;
                }
                _geralPersistence.Update(telemetry);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTelemetry(int id)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                if (telemetry == null)
                {
                    throw new Exception("Maquina para delete não foi encontrado!!");
                }
                _geralPersistence.Delete<Telemetry>(telemetry);

                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Telemetry[]> GetAllTelemetriesAsync()
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetriesAsync();
                if (telemetry == null) return null;
                return telemetry;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Telemetry[]> GetAllTelemetriesByStatusAsync(MachineStatus status)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetriesByStatusAsync(status);
                if (telemetry == null) return null;
                return telemetry;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Telemetry> GetTelemetriesByIdAsync(int id)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                if (telemetry == null) return null;
                return telemetry;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}