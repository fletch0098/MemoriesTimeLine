import { Component, OnInit } from '@angular/core';

import { TimeLineService } from '../services/time-line.service';

import { timeLine } from '../models/timeLine';
import { memory } from '../models/memory';

@Component({
  selector: 'app-time-lines',
  templateUrl: './time-lines.component.html',
  styleUrls: ['./time-lines.component.css']
})
export class TimeLinesComponent implements OnInit {

  timelines: timeLine[];

  constructor(private timeLineService: TimeLineService) {}

  ngOnInit() {

    this.timeLineService.getTimeLines()
      .subscribe((timelines: timeLine[]) => {
        this.timelines = timelines;
      },
        error => {
          //this.notificationService.printErrorMessage(error);
        });

  }

}
