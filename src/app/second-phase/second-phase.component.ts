import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-second-phase',
  templateUrl: './second-phase.component.html',
  styleUrls: ['./second-phase.component.scss']
})
export class SecondPhaseComponent implements OnInit {
  doctorTable!: MatTableDataSource<any>;
  constructor() { }

  ngOnInit(): void {
  }

}
