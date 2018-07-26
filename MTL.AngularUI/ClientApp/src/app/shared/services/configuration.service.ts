import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { appSetting } from '../Models/appSettings';

@Injectable()
export class ConfigurationService {

  _appSettings: appSetting;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

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

}

