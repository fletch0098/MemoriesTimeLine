import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }        from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';

import { TimeLineService } from './services/time-line.service';

import { routing }  from './timeline.routing';
import { RootComponent } from './root/root.component';
import { HomeComponent } from './home/home.component';

import { AuthGuard } from '../auth.guard';
import { TimeLineComponent } from './time-line/time-line.component';
import { TimeLinesComponent } from './time-lines/time-lines.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    routing,
    SharedModule
  ],
  declarations: [RootComponent, HomeComponent, TimeLineComponent, TimeLinesComponent],
  exports:      [ ],
  providers: [AuthGuard, TimeLineService]
})
export class TimeLineModule { }
