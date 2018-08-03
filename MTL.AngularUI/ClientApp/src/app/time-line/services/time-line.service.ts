import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';
import '../../rxjs-operators';

import { BaseService } from "../../shared/services/base.service";
import { ConfigurationService } from '../../shared/services/configuration.service';

import { timeLine } from '../models/timeLine';
import { memory } from '../models/memory';

@Injectable()

export class TimeLineService extends BaseService {

  baseUrl: string = '';

  // Observable navItem source
  //private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  //authNavStatus$ = this._authNavStatusSource.asObservable();

  //private loggedIn = false;

  constructor(private http: Http, private configService: ConfigurationService) {
    super();
    //this.loggedIn = !!localStorage.getItem('auth_token');
    //this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getWebApiURL();
  }

  //newTimeLine(email: string, password: string, firstName: string, lastName: string,location: string): Observable<UserRegistration> {
  //  let body = JSON.stringify({ email, password, firstName, lastName,location });
  //  let headers = new Headers({ 'Content-Type': 'application/json' });
  //  let options = new RequestOptions({ headers: headers });

  //  return this.http.post(this.baseUrl + "api/accounts", body, options)
  //    .map(res => true)
  //    .catch(this.handleError);
  //}  

  getTimeLines(): Observable<timeLine[]> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    //headers.append('Authorization', `Bearer ${authToken}`);

    return this.http.get(this.baseUrl + "api/timeline/", { headers })
      .map(response => response.json())
      .catch(this.handleError);
  }

  getMyTimeLines(): Observable<timeLine[]> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);

    return this.http.get(this.baseUrl + "api/timeline/GetMyTimeLines", { headers })
      .map(response => response.json())
      .catch(this.handleError);
  }

  getTimeLine(id: number): Observable<timeLine> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);

    //const url = `${this.baseUrl}/${id}`

    return this.http.get(this.baseUrl + "api/timeline/get/" + id, { headers })
      .map(response => response.json())
      .catch(this.handleError);
  }  
}

