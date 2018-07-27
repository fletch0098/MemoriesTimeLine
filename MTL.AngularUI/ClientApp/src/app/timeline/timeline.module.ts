import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimelineService } from './timeline.service';
import { TimelineRoutingModule } from './/timeline-routing.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    CommonModule,
    TimelineRoutingModule
  ],
  declarations: [HomeComponent],
  providers: [TimelineService]
})
export class TimelineModule { }
