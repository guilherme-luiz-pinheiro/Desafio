import { Component, OnInit } from '@angular/core';
import { MachineService } from '../../services/machine.service';
import { Router } from '@angular/router';
import { Machine } from '../../models/machine.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  machines: Machine[] = [];  // Array que armazenará as máquinas carregadas
  loading = false;           // Flag para controlar o estado de carregamento da lista

  // Injeta o serviço para acessar as APIs e o roteador para navegação
  constructor(private service: MachineService, private router: Router) {}

  // Ciclo de vida Angular: Executado após a criação do componente
  ngOnInit() {
    this.loadMachines(); // Carrega as máquinas assim que o componente é inicializado
  }

  // Método para buscar as máquinas usando o serviço
  loadMachines() {
    this.loading = true; // Ativa indicador de carregamento
    this.service.getMachines().subscribe({
      next: data => {
        this.machines = data;  // Atualiza o array de máquinas com os dados recebidos
        this.loading = false;  // Desativa indicador de carregamento
      },
      error: err => {
        alert('Erro ao carregar máquinas.'); // Exibe alerta em caso de erro
        this.loading = false;  // Desativa indicador de carregamento mesmo com erro
      }
    });
  }

  // Remove uma máquina após confirmação do usuário
  deleteMachine(id: string | undefined) {
    if (!id) return;  // Sai se não receber um id válido
    if (confirm('Confirma exclusão desta máquina?')) {
      this.service.deleteMachine(id).subscribe(() => {
        alert('Máquina excluída!');  // Confirmação de exclusão
        this.loadMachines();         // Recarrega a lista após exclusão
      }, () => alert('Erro ao excluir máquina.'));  // Alerta se houver erro na exclusão
    }
  }

  // Navega para a tela de edição da máquina
  editMachine(id: string | undefined) {
    if (!id) return; // Sai se o id for inválido
    this.router.navigate(['/machines/edit/', id]); // Navega para rota de edição com o id da máquina
  }

  // Navega para a tela de criação de uma nova máquina
  createMachine() {
    this.router.navigate(['/machines/create']);
  }

  // Método auxiliar para transformar o status numérico em texto legível
  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Parada';
      case 1: return 'Manutenção';
      case 2: return 'Operando';
      default: return 'Desconhecido';
    }
  }
}
