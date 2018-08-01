import { Component, OnInit } from '@angular/core';

import { timeLine } from '../models/timeLine';
import { memory } from '../models/memory';

@Component({
  selector: 'app-time-line',
  templateUrl: './time-line.component.html',
  styleUrls: ['./time-line.component.css']
})
export class TimeLineComponent implements OnInit {

  timeLine: timeLine;
  memories: memory[] = new Array<memory>();

  constructor() { }

  ngOnInit() {
  }

  addMemory() {
    let memory: memory = { name: "newMemory", description: "this is a test memory" };
    this.memories.push(memory);
  }

}
