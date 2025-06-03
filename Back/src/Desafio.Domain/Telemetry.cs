using System;
using Desafio.Domain.Enums;

namespace Desafio.Domain
{
    /// <summary>
    /// Representa um registro de telemetria de uma máquina em determinado momento.
    /// </summary>
    public class Telemetry
    {
        /// <summary>
        /// Identificador único da telemetria (chave primária).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Chave estrangeira que relaciona esta telemetria com uma máquina.
        /// </summary>
        public Guid MachineId { get; set; }

        /// <summary>
        /// Localização da máquina no momento do envio da telemetria.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Status da máquina (operando, manutenção, parada) no momento da telemetria.
        /// </summary>
        public MachineStatus Status { get; set; }

        /// <summary>
        /// Momento exato em que a telemetria foi capturada.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Construtor com parâmetros, útil para facilitar criação de instâncias com dados completos.
        /// </summary>
        public Telemetry(Guid machineId, MachineStatus status, string location, DateTime timestamp)
        {
            Id = 0; // Normalmente será sobrescrito pelo banco de dados.
            MachineId = machineId;
            Status = status;
            Location = location;
            Timestamp = timestamp;            
        }

        /// <summary>
        /// Construtor sem parâmetros, necessário para operações do Entity Framework.
        /// </summary>
        public Telemetry()
        {
        }
    }
}
