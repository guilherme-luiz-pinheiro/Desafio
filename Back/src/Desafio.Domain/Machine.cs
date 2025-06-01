using System;
using System.Collections.Generic;
using Desafio.Domain.Enums;

namespace Desafio.Domain
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public MachineStatus Status { get; set; }
        public IEnumerable<Telemetry> Telemetries { get; set; }

    }
}
