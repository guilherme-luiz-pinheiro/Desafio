import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Machine } from '../models/machine.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
// Serviço para manipulação dos dados das máquinas via API REST
export class MachineService {
  // URL base da API para recursos de máquinas
  private baseUrl = 'https://localhost:5001/api/machines';

  constructor(private http: HttpClient) {}

  // Busca todas as máquinas (GET /api/machines)
  getMachines(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.baseUrl);
  }  

  // Busca uma máquina específica pelo ID (GET /api/machines/{id})
  getMachine(id: string): Observable<Machine> {
    return this.http.get<Machine>(`${this.baseUrl}/${id}`);
  }

  // Busca máquinas filtrando pelo status (GET /api/machines?status={status})
  getMachinesByStatus(status: number): Observable<Machine[]> {
    // Utiliza query string para enviar filtro por status
    return this.http.get<Machine[]>(`${this.baseUrl}?status=${status}`);
  }
  
  // Cria uma nova máquina (POST /api/machines)
  createMachine(machine: Partial<Machine>): Observable<Machine> {
    // Partial<Machine> permite passar objeto parcial (sem todos os campos)
    return this.http.post<Machine>(this.baseUrl, machine);
  }

  // Atualiza uma máquina existente (PUT /api/machines)
  updateMachine(machine: Machine): Observable<Machine> {
    return this.http.put<Machine>(this.baseUrl, machine);
  }

  // Deleta uma máquina pelo ID (DELETE /api/machines/{id})
  deleteMachine(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
