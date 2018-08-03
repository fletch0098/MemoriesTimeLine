import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigurationService } from '../shared/services/configuration.service';
import { SpinnerComponent } from '../shared/Components/spinner/spinner.component';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  public forecasts: WeatherForecast[];
  private apiURL: string;
  isRequesting: boolean; 

  constructor(http: HttpClient, private configService: ConfigurationService) {

    this.isRequesting = true;
    this.apiURL = configService.getWebApiURL();

    http.get<WeatherForecast[]>(this.apiURL + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
      this.isRequesting = false;
    }, error => console.error(error));

  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
