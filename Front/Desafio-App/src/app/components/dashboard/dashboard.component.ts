import { Component, OnInit } from '@angular/core';
import { MachineService } from '../../services/machine.service';
import { Router } from '@angular/router';
import { Machine } from '../../models/machine.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  machines: Machine[] = [];
  loading = false;

  constructor(private service: MachineService, private router: Router) {}

  ngOnInit() {
    this.loadMachines();
  }

  loadMachines() {
    this.loading = true;
    this.service.getMachines().subscribe({
      next: data => {
        this.machines = data;
        this.loading = false;
      },
      error: err => {
        alert('Erro ao carregar máquinas.');
        this.loading = false;
      }
    });
  }

  deleteMachine(id: string | undefined) {
    if (!id) return;
    if (confirm('Confirma exclusão desta máquina?')) {
      this.service.deleteMachine(id).subscribe(() => {
        alert('Máquina excluída!');
        this.loadMachines();
      }, () => alert('Erro ao excluir máquina.'));
    }
  }

  editMachine(id: string | undefined) {
    if (!id) return;
    this.router.navigate(['/machines/edit/', id]);
  }

  createMachine() {
    this.router.navigate(['/machines/create']);
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Parada';
      case 1: return 'Manutenção';
      case 2: return 'Operando';
      default: return 'Desconhecido';
    }
  }
}
