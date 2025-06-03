export interface Machine {
  id: string;         // Identificador único da máquina (UUID)
  name: string;       // Nome ou identificação amigável da máquina
  location: string;   // Localização da máquina (pode ser coordenadas GPS ou nome do local)
  status: number;     // Status atual da máquina, onde:
                      // 0 = parada,
                      // 1 = manutenção,
                      // 2 = operando
  telemetries?: Telemetry[]; // Lista opcional de telemetrias associadas à máquina
}

export interface Telemetry {
  id: number;          // Identificador único da telemetria (geralmente autoincrementado)
  machineId: string;   // ID da máquina associada a essa telemetria (UUID)
  location: string;    // Localização onde a telemetria foi coletada
  status: number;      // Status da máquina naquele instante (mesma codificação do Machine.status)
  timestamp: string;   // Data e hora da telemetria no formato ISO 8601 (exemplo: "2025-06-02T15:00:00Z")
}
