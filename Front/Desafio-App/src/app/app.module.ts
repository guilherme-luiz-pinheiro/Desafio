import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser'; // Módulo para rodar app no navegador
import { HttpClientModule } from '@angular/common/http';  // Módulo para comunicação HTTP
import { AppRoutingModule } from './app-routing.module'; // Módulo que contém as rotas da aplicação
import { RouterModule, Routes } from '@angular/router';  // Import para configurar rotas (não usado aqui)

import { ReactiveFormsModule } from '@angular/forms';    // Módulo para formulários reativos

// Componentes da aplicação
import { AppComponent } from './app.component';
import { MachineFormComponent } from './components/machine-form/machine-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';  // Dashboard principal
import { MachineDetailsComponent } from './components/machine-details/machine-details.component';
import { EditMachineComponent } from './components/machine-edit/edit-machine.component';

// Definição de rotas local (não usado, pois as rotas estão no AppRoutingModule)
const routes: Routes = [
  { path: 'machines/:id', component: MachineDetailsComponent },
  // outras rotas...
];

@NgModule({
  declarations: [
    // Declaração dos componentes usados na aplicação
    AppComponent,
    MachineFormComponent,
    EditMachineComponent,
    MachineDetailsComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,       // Necessário para apps web Angular rodarem no browser
    AppRoutingModule,    // Importa rotas definidas no módulo de roteamento
    HttpClientModule,    // Permite comunicação HTTP com APIs externas
    ReactiveFormsModule  // Permite o uso de formulários reativos no app
  ],
  providers: [],         // Serviços que serão injetados na aplicação (nenhum por enquanto)
  bootstrap: [AppComponent] // Componente raiz que será inicializado ao carregar o app
})
export class AppModule { }
