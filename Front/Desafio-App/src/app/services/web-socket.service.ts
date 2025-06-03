import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';

// Interface para tipar as atualizações que chegam via WebSocket
export interface MachineUpdate {
  machineId: string; // ID da máquina que foi atualizada
  status: number;    // Novo status da máquina (pode ser enum numerado)
  location?: string; // Localização opcional da máquina
}

@Injectable({
  providedIn: 'root'  // Serviço singleton disponível em toda a aplicação
})
export class WebSocketService implements OnDestroy {
  private socket!: WebSocket;           // Instância do WebSocket
  private subject = new Subject<MachineUpdate>(); // Subject para emitir as atualizações recebidas
  private readonly WS_URL = 'ws://localhost:5000/ws'; // URL do servidor WebSocket (ajustar conforme backend)

  // Método para conectar ao WebSocket e retornar um Observable que emite as atualizações
  connect(): Observable<MachineUpdate> {
    this.socket = new WebSocket(this.WS_URL);

    // Evento disparado quando a conexão é aberta
    this.socket.onopen = () => {
      console.log('WebSocket conectado');
    };

    // Evento disparado quando uma mensagem é recebida do servidor
    this.socket.onmessage = (event) => {
      try {
        // Tenta converter a mensagem JSON para o formato MachineUpdate
        const data = JSON.parse(event.data) as MachineUpdate;
        // Emite a atualização para todos os inscritos
        this.subject.next(data);
      } catch (error) {
        console.error('Erro ao parsear mensagem do WebSocket:', error);
      }
    };

    // Evento disparado em caso de erro na conexão WebSocket
    this.socket.onerror = (error) => {
      console.error('Erro no WebSocket:', error);
    };

    // Evento disparado quando a conexão é fechada
    this.socket.onclose = (event) => {
      console.log(`WebSocket desconectado: code=${event.code} reason=${event.reason}`);
      // Aqui você pode implementar lógica de reconexão automática se quiser
    };

    // Retorna o Observable para que componentes possam se inscrever nas atualizações
    return this.subject.asObservable();
  }

  // Método para desconectar o WebSocket manualmente
  disconnect(): void {
    if (this.socket && this.socket.readyState === WebSocket.OPEN) {
      this.socket.close();
    }
  }

  // Método do ciclo de vida do Angular para garantir desconexão e limpeza quando o serviço for destruído
  ngOnDestroy(): void {
    this.disconnect();
    this.subject.complete();
  }
}
