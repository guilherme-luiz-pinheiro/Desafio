import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Machine } from '../models/machine.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class MachineService {
  private baseUrl = 'https://localhost:5001/api/machines';

  constructor(private http: HttpClient) {}

  getMachines(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.baseUrl);
  }  

  getMachine(id: string): Observable<Machine> {
    return this.http.get<Machine>(`${this.baseUrl}/${id}`);
  }

  getMachinesByStatus(status: number): Observable<Machine[]> {
    // Ajustei para usar query param para status
    return this.http.get<Machine[]>(`${this.baseUrl}?status=${status}`);
  }
  
  createMachine(machine: Partial<Machine>): Observable<Machine> {
    return this.http.post<Machine>(this.baseUrl, machine);
  }

  updateMachine(machine: Machine): Observable<Machine> {
  return this.http.put<Machine>(this.baseUrl, machine);
}

  deleteMachine(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
