import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';

import { HomeDetails } from '../models/home.details.interface';
import { ConfigurationService } from '../../shared/services/configuration.service';

import {BaseService} from '../../shared/services/base.service';

import { Observable } from 'rxjs/Rx'; 

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';

@Injectable()

export class DashboardService extends BaseService {

  baseUrl: string = ''; 

  constructor(private http: Http, private configService: ConfigurationService) {
     super();
     this.baseUrl = configService.getWebApiURL();
  }

  getHomeDetails(): Observable<HomeDetails> {
      let headers = new Headers();
      headers.append('Content-Type', 'application/json');
      let authToken = localStorage.getItem('auth_token');
      headers.append('Authorization', `Bearer ${authToken}`);
  
    return this.http.get(this.baseUrl + "api/dashboard/home",{headers})
      .map(response => response.json())
      .catch(this.handleError);
  }  
}
