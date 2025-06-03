import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';  // Para acessar parâmetros da rota
import { MachineService } from '../../services/machine.service';
import { Machine } from '../../models/machine.model';

@Component({
  selector: 'app-machine-details',
  templateUrl: './machine-details.component.html',
  styleUrls: ['./machine-details.component.scss']
})
export class MachineDetailsComponent implements OnInit {
  public machine?: Machine;  // Armazena os dados da máquina carregada

  constructor(
    private route: ActivatedRoute,       // Injeta o serviço para obter parâmetros da URL
    private machineService: MachineService  // Injeta o serviço para comunicação com API
  ) {}

  ngOnInit(): void {
    // Obtém o parâmetro 'id' da rota atual
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      // Se existir um id, busca os detalhes da máquina pelo serviço
      this.machineService.getMachine(id).subscribe({
        next: (data) => this.machine = data,  // Ao receber dados, armazena na propriedade machine
        error: (err) => console.error('Erro ao buscar máquina', err)  // Loga erro no console
      });
    }
  }

  // Retorna um texto legível para o status da máquina
  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Parada';
      case 1: return 'Manutenção';
      case 2: return 'Operando';
      default: return 'Desconhecido';
    }
  }

  // Retorna uma classe CSS para estilizar o status, com cores diferentes
  getStatusClass(status: number): string {
    switch (status) {
      case 0: return 'badge bg-secondary';          // Cinza para Parada
      case 1: return 'badge bg-warning text-dark';  // Amarelo para Manutenção
      case 2: return 'badge bg-success';             // Verde para Operando
      default: return 'badge bg-danger';             // Vermelho para status desconhecido
    }
  }
}
