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
    let dateNow: Date = new Date();
    let memory: memory = { id: 1, timeLineId: 1, name: "newMemory", description: "this is a test memory", date: dateNow, lastModified: '' };
    this.memories.push(memory);
  }

}
