import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {ConfigurationService } from '../shared/services/configuration.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  private apiURL: string;

  constructor(http: HttpClient, private configService: ConfigurationService) {

    this.apiURL = configService.getWebApiURL();

    http.get<WeatherForecast[]>(this.apiURL + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
