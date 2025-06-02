import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MachineService } from '../../services/machine.service';
import { Machine } from '../../models/machine.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-machine',
  templateUrl: './edit-machine.component.html',
})
export class EditMachineComponent implements OnInit {
  machineForm!: FormGroup;
  machineId!: string;

  constructor(
    private route: ActivatedRoute,
    private service: MachineService,
    private fb: FormBuilder,
    public router: Router
  ) {}

  ngOnInit() {
    this.machineId = this.route.snapshot.paramMap.get('id')!;
    this.initForm();

    this.service.getMachine(this.machineId).subscribe({
      next: (machine) => this.machineForm.patchValue(machine),
      error: () => alert('Erro ao carregar máquina'),
    });
  }

  initForm() {
    this.machineForm = this.fb.group({
      name: ['', Validators.required],
      location: ['', Validators.required],
      status: [0, Validators.required],
    });
  }

  save() {
  if (this.machineForm.invalid) return;

  const updatedMachine: Machine = {
    id: this.machineId,
    ...this.machineForm.value,
  };
    console.log('Dados enviados para o backend:', updatedMachine);


  this.service.updateMachine(updatedMachine).subscribe({
    next: () => {
      alert('Máquina atualizada com sucesso!');
      this.router.navigate(['/dashboard']);
    },
    error: () => alert('Erro ao atualizar máquina.'),
  });
}

}
