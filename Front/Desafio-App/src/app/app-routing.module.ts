import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MachineDetailsComponent } from './components/machine-details/machine-details.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EditMachineComponent } from './components/machine-edit/edit-machine.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'machines/:id', component: MachineDetailsComponent },
  { path: 'machines/edit/:id', component: EditMachineComponent },

  // outras rotas, se tiver
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
