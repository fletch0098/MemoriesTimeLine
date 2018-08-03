import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx'; 

import { appSetting } from '../Models/appSettings';
import { facebookSettings } from '../Models/facebookSettings';
import { BaseService } from '../../shared/services/base.service';

@Injectable()
export class ConfigurationService extends BaseService {

  _appSettings: appSetting;
  _facebookSettings: facebookSettings;

  constructor(private httpClient: HttpClient, private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    super();
  }

  loadConfig() {
    return this.httpClient.get<appSetting>(this.baseUrl + 'api/configuration/getAppSettings')
      .toPromise()
      .then(result => {
        console.log('configurationService - loadConfig: Success');
        this._appSettings = result;
      }, error => console.error(error));
  }

  getAppSettings() {
    return this._appSettings;
  }

  getWebApiURL() {
    return this._appSettings.apiURL;
  }

  getAppURL() {
    return this._appSettings.appURL;
  }

  //getFacebookSettings() {

  //  this.httpClient.get<facebookSettings>(this.baseUrl + 'api/configuration/GetFacebookAuthSettings').subscribe(result => {

  //  }, error => console.error(error));

  //}

  getFacebookSettings(): Observable<facebookSettings> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http.get(this.baseUrl + "api/configuration/GetFacebookAuthSettings", { headers })
      .map(response => response.json())
      .catch(this.handleError);
  }  

}

