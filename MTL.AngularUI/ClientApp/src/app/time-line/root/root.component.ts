import { Component, OnInit } from '@angular/core';

 

@Component({
  selector: 'app-root',
  templateUrl: './root.component.html',
  styleUrls: ['./root.component.scss'],
 
})
export class RootComponent implements OnInit {
  status: boolean = false;
  constructor() { }

  ngOnInit() {
  }

  toggleSideBar() {
    this.status = !this.status; 
  }

}
