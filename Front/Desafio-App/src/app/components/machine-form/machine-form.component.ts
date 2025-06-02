import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MachineService } from '../../services/machine.service';
import { Machine } from '../../models/machine.model';

import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-machine-form',
  templateUrl: './machine-form.component.html'
})
export class MachineFormComponent implements OnInit {
  form = this.fb.group({
    name: ['', Validators.required],
    location: ['', Validators.required],
    status: [2, Validators.required]
  });
  isEditMode = false;
  machineId?: string;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private service: MachineService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.machineId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.machineId) {
      this.isEditMode = true;
      this.loadMachine(this.machineId);
    }
  }

  loadMachine(id: string) {
    this.loading = true;
    this.service.getMachine(id).subscribe({
      next: machine => {
        this.form.patchValue(machine);
        this.loading = false;
      },
      error: () => {
        alert('Erro ao carregar máquina.');
        this.loading = false;
      }
    });
  }

  submit() {
    if (this.form.invalid) return;

    const machine: Machine = this.form.value;
    this.loading = true;

    if (this.isEditMode && this.machineId) {
      this.service.updateMachine(machine).subscribe({
        next: () => {
          alert('Máquina atualizada!');
          this.loading = false;
          this.router.navigate(['/machines']);
        },
        error: () => {
          alert('Erro ao atualizar máquina.');
          this.loading = false;
        }
      });
    } else {
      this.service.createMachine(machine).subscribe({
        next: () => {
          alert('Máquina cadastrada!');
          this.loading = false;
          this.form.reset({ status: 2 });
          this.router.navigate(['/machines']);
        },
        error: () => {
          alert('Erro ao cadastrar máquina.');
          this.loading = false;
        }
      });
    }
  }

  deleteMachine() {
    if (!this.machineId) return;
    if (confirm('Tem certeza que deseja excluir esta máquina?')) {
      this.loading = true;
      this.service.deleteMachine(this.machineId).subscribe({
        next: () => {
          alert('Máquina excluída!');
          this.loading = false;
          this.router.navigate(['/machines']);
        },
        error: () => {
          alert('Erro ao excluir máquina.');
          this.loading = false;
        }
      });
    }
  }
}
