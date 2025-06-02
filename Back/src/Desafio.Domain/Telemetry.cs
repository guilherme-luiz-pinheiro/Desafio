using System;
using Desafio.Domain.Enums;

namespace Desafio.Domain
{
    public class Telemetry
    {
        public int Id { get; set; }

        public Guid MachineId { get; set; }

        public string Location { get; set; }
        public MachineStatus Status { get; set; }

        public DateTime Timestamp { get; set; }

        public Telemetry(Guid machineId, MachineStatus status, string location, DateTime timestamp)
        {
            Id=0;
            MachineId = machineId;
            Status =status;
            Location=location;
            Timestamp=timestamp;            
        }
        public Telemetry()
        {
            
        }
    }
}
