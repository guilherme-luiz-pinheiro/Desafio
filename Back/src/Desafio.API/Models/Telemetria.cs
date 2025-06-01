using System;
using Desafio.API.Models.Enums;

namespace Desafio.API.Models
{
    public class Telemetry
    {
        public int Id { get; set; }

        public Guid MachineId { get; set; }
        public Machine Machine { get; set; }

        public string Location { get; set; }
        public MachineStatus Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
