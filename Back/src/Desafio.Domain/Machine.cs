using System;
using System.Collections.Generic;
using Desafio.Domain.Enums;

namespace Desafio.Domain
{
    /// <summary>
    /// Representa uma máquina no sistema, contendo informações básicas
    /// como nome, localização, status atual e uma coleção de telemetrias associadas.
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Identificador único da máquina (chave primária).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da máquina (ex: "Escavadeira 1").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Localização atual da máquina (ex: "Setor A").
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Status operacional da máquina, baseado no enum MachineStatus.
        /// </summary>
        public MachineStatus Status { get; set; }

        /// <summary>
        /// Lista de registros de telemetria associados à máquina.
        /// Relacionamento 1:N entre Machine e Telemetry.
        /// </summary>
        public ICollection<Telemetry> Telemetries { get; set; } = new List<Telemetry>();
    }
}
