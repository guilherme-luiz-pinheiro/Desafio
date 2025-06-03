import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';  // Para obter parâmetros da rota e navegação
import { MachineService } from '../../services/machine.service';
import { Machine } from '../../models/machine.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';  // Para criação e validação de formulários

@Component({
  selector: 'app-edit-machine',
  templateUrl: './edit-machine.component.html',
})
export class EditMachineComponent implements OnInit {
  machineForm!: FormGroup;   // Formulário reativo para editar a máquina
  machineId!: string;        // Guarda o ID da máquina que será editada

  constructor(
    private route: ActivatedRoute,  // Para capturar o parâmetro 'id' da URL
    private service: MachineService, // Serviço para comunicação com backend
    private fb: FormBuilder,         // Builder para criar o formulário reativo
    public router: Router            // Para navegação programática após salvar
  ) {}

  ngOnInit() {
    // Obtém o 'id' da máquina a partir da URL
    this.machineId = this.route.snapshot.paramMap.get('id')!;

    // Inicializa o formulário com validações
    this.initForm();

    // Busca os dados da máquina no backend e preenche o formulário
    this.service.getMachine(this.machineId).subscribe({
      next: (machine) => this.machineForm.patchValue(machine),
      error: () => alert('Erro ao carregar máquina'),
    });
  }

  // Configura o formulário reativo com campos e validações obrigatórias
  initForm() {
    this.machineForm = this.fb.group({
      name: ['', Validators.required],
      location: ['', Validators.required],
      status: [0, Validators.required],
    });
  }

  // Função para salvar as alterações da máquina
  save() {
    if (this.machineForm.invalid) return;  // Não salva se o formulário estiver inválido

    // Cria um objeto com os dados atualizados, incluindo o id da máquina
    const updatedMachine: Machine = {
      id: this.machineId,
      ...this.machineForm.value,
    };

    console.log('Dados enviados para o backend:', updatedMachine);

    // Chama o serviço para atualizar a máquina no backend
    this.service.updateMachine(updatedMachine).subscribe({
      next: () => {
        alert('Máquina atualizada com sucesso!');
        this.router.navigate(['/dashboard']);  // Redireciona para o dashboard após salvar
      },
      error: () => alert('Erro ao atualizar máquina.'),
    });
  }
}
