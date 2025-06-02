import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';

export interface MachineUpdate {
  machineId: string;
  status: number;
  location?: string;
}

@Injectable({
  providedIn: 'root'
})
export class WebSocketService implements OnDestroy {
  private socket!: WebSocket;
  private subject = new Subject<MachineUpdate>();
  private readonly WS_URL = 'ws://localhost:5000/ws'; // Ajuste a URL conforme seu backend

  connect(): Observable<MachineUpdate> {
    this.socket = new WebSocket(this.WS_URL);

    this.socket.onopen = () => {
      console.log('WebSocket conectado');
    };

    this.socket.onmessage = (event) => {
      try {
        const data = JSON.parse(event.data) as MachineUpdate;
        this.subject.next(data);
      } catch (error) {
        console.error('Erro ao parsear mensagem do WebSocket:', error);
      }
    };

    this.socket.onerror = (error) => {
      console.error('Erro no WebSocket:', error);
    };

    this.socket.onclose = (event) => {
      console.log(`WebSocket desconectado: code=${event.code} reason=${event.reason}`);
      // Aqui você pode implementar reconexão se desejar
    };

    return this.subject.asObservable();
  }

  disconnect(): void {
    if (this.socket && this.socket.readyState === WebSocket.OPEN) {
      this.socket.close();
    }
  }

  ngOnDestroy(): void {
    this.disconnect();
    this.subject.complete();
  }
}
