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
  // Formulário reativo com campos: name, location e status (padrão 2 = operando)
  form = this.fb.group({
    name: ['', Validators.required],
    location: ['', Validators.required],
    status: [2, Validators.required]
  });

  isEditMode = false;       // Indica se o formulário está em modo edição
  machineId?: string;       // Guarda o ID da máquina (se for edição)
  loading = false;          // Controle de estado para mostrar carregamento

  constructor(
    private fb: FormBuilder,        // Builder para criar formulário reativo
    private service: MachineService, // Serviço para comunicação com backend
    private route: ActivatedRoute,  // Para pegar parâmetros da rota
    private router: Router          // Para navegação programada
  ) {}

  ngOnInit() {
    // Verifica se existe parâmetro 'id' na rota para modo edição
    this.machineId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.machineId) {
      this.isEditMode = true;
      this.loadMachine(this.machineId);
    }
  }

  // Busca dados da máquina para preencher o formulário em modo edição
  loadMachine(id: string) {
    this.loading = true;
    this.service.getMachine(id).subscribe({
      next: machine => {
        this.form.patchValue(machine);  // Atualiza formulário com dados
        this.loading = false;
      },
      error: () => {
        alert('Erro ao carregar máquina.');
        this.loading = false;
      }
    });
  }

  // Envia dados para criar ou atualizar a máquina conforme o modo
  submit() {
    if (this.form.invalid) return;  // Não envia se formulário inválido

    const machine: Machine = this.form.value;
    this.loading = true;

    if (this.isEditMode && this.machineId) {
      // Atualiza máquina existente
      this.service.updateMachine(machine).subscribe({
        next: () => {
          alert('Máquina atualizada!');
          this.loading = false;
          this.router.navigate(['/machines']);  // Navega para lista de máquinas
        },
        error: () => {
          alert('Erro ao atualizar máquina.');
          this.loading = false;
        }
      });
    } else {
      // Cria nova máquina
      this.service.createMachine(machine).subscribe({
        next: () => {
          alert('Máquina cadastrada!');
          this.loading = false;
          this.form.reset({ status: 2 }); // Reseta formulário com status padrão
          this.router.navigate(['/machines']);  // Navega para lista de máquinas
        },
        error: () => {
          alert('Erro ao cadastrar máquina.');
          this.loading = false;
        }
      });
    }
  }

  // Exclui máquina atual (modo edição)
  deleteMachine() {
    if (!this.machineId) return;
    if (confirm('Tem certeza que deseja excluir esta máquina?')) {
      this.loading = true;
      this.service.deleteMachine(this.machineId).subscribe({
        next: () => {
          alert('Máquina excluída!');
          this.loading = false;
          this.router.navigate(['/machines']);  // Navega para lista de máquinas
        },
        error: () => {
          alert('Erro ao excluir máquina.');
          this.loading = false;
        }
      });
    }
  }
}
