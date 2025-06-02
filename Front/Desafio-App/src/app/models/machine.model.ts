export interface Machine {
  id: string;         // UUID
  name: string;
  location: string;   // Pode ser coordenadas ou nome do local
  status: number;     // 0 = parada, 1 = manutenção, 2 = operando
  telemetries?: Telemetry[];
}

export interface Telemetry {
  id: number;
  machineId: string;
  location: string;
  status: number;
  timestamp: string; // ISO string para data/hora
}
