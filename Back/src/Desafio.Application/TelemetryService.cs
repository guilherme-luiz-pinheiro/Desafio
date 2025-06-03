using System;
using System.Threading.Tasks;
using Desafio.Application.Interfaces;
using Desafio.Domain;
using Desafio.Domain.Enums;
using Desafio.Persistence.Interfaces;

namespace Desafio.Application
{
    // Implementação da lógica de negócios relacionada à Telemetria das máquinas
    public class TelemetryService : ITelemetryService
    {
        private readonly IGeralPersistence _geralPersistence;                 // Interface genérica de persistência (Add, Update, Delete, Save)
        private readonly ITelemetryPersistence _telemetryPersistence;         // Interface específica de leitura de telemetrias

        public TelemetryService(IGeralPersistence geralPersistence,
                                ITelemetryPersistence machinePersistence)
        {
            _geralPersistence = geralPersistence;
            _telemetryPersistence = machinePersistence;
        }

        /// <summary>
        /// Adiciona uma nova telemetria ao banco de dados.
        /// </summary>
        public async Task<Telemetry> AddTelemetry(Telemetry model)
        {
            try
            {
                _geralPersistence.Add<Telemetry>(model); // Adiciona ao contexto
                if (await _geralPersistence.SaveChangesAsync())
                {
                    // Retorna a telemetria salva, buscando pelo ID
                    return await _telemetryPersistence.GetAllTelemetryByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma telemetria existente, sem alterar seus valores.
        /// OBS: Esse método está incompleto em termos de atualização real (apenas reenviando o que já existe).
        /// </summary>
        public async Task<Telemetry> UpdateTelemetry(int id)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                if (telemetry == null)
                {
                    return null;
                }

                _geralPersistence.Update(telemetry); // Atualiza (sem alterar propriedades)
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

        /// <summary>
        /// Exclui uma telemetria pelo ID.
        /// </summary>
        public async Task<bool> DeleteTelemetry(int id)
        {
            try
            {
                var telemetry = await _telemetryPersistence.GetAllTelemetryByIdAsync(id);
                if (telemetry == null)
                {
                    throw new Exception("Telemetria para deletar não foi encontrada!");
                }

                _geralPersistence.Delete<Telemetry>(telemetry); // Remove do contexto
                return await _geralPersistence.SaveChangesAsync(); // Salva alteração no banco
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todas as telemetrias existentes.
        /// </summary>
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

        /// <summary>
        /// Retorna todas as telemetrias com um determinado status da máquina.
        /// </summary>
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

        /// <summary>
        /// Retorna uma telemetria específica pelo ID.
        /// </summary>
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
