import { Component, OnInit } from '@angular/core';
import { appSetting } from '../shared/Models/appSettings';
import { ConfigurationService } from '../shared/services/configuration.service';


@Component({
  selector: 'app-app-settings',
  templateUrl: './app-settings.component.html',
  styleUrls: ['./app-settings.component.css']
})
export class AppSettingsComponent implements OnInit {

  appSettings: appSetting;

  constructor(private configurationService: ConfigurationService) {
    this.appSettings = this.configurationService.getAppSettings();
  }

  ngOnInit() {
    
  }

}
