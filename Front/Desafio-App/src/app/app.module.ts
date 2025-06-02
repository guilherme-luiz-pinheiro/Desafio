import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule, Routes } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { MachineFormComponent } from './components/machine-form/machine-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';  // <-- importe o DashboardComponent
import { MachineDetailsComponent } from './components/machine-details/machine-details.component';
import { EditMachineComponent } from './components/machine-edit/edit-machine.component';

const routes: Routes = [
  { path: 'machines/:id', component: MachineDetailsComponent },
  // outras rotas...
];

@NgModule({
  declarations: [
    AppComponent,
    MachineFormComponent,
    EditMachineComponent,
    MachineDetailsComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,  // <-- só ele
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

