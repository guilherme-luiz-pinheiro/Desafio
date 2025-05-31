import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss']
})
export class MachineComponent implements OnInit {

  constructor(private http: HttpClient) { }

  public machines: any;

  ngOnInit(): void {   
     this.getMachines();
  }
  public getMachines(): void{
    this.http.get('https://localhost:5001/api/machines').subscribe(
      response => this.machines = response,
      error => {
        return console.log(error);
      }
    );
  }

}
