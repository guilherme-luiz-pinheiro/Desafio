import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MachineService } from '../../services/machine.service';
import { Machine } from '../../models/machine.model';

@Component({
  selector: 'app-machine-details',
  templateUrl: './machine-details.component.html',
  styleUrls: ['./machine-details.component.scss']
})
export class MachineDetailsComponent implements OnInit {
  public machine?: Machine;

  constructor(
    private route: ActivatedRoute,
    private machineService: MachineService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.machineService.getMachine(id).subscribe({
        next: (data) => this.machine = data,
        error: (err) => console.error('Erro ao buscar máquina', err)
      });
    }
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'Parada';
      case 1: return 'Manutenção';
      case 2: return 'Operando';
      default: return 'Desconhecido';
    }
  }

  getStatusClass(status: number): string {
    switch (status) {
      case 0: return 'badge bg-secondary';
      case 1: return 'badge bg-warning text-dark';
      case 2: return 'badge bg-success';
      default: return 'badge bg-danger';
    }
  }
}
