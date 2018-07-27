import { Component } from '@angular/core';

import { UserService } from '../../shared/services/user.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { facebookSettings } from '../../shared/Models/facebookSettings';

import { Router } from '@angular/router';
 

@Component({
  selector: 'app-facebook-login',
  templateUrl: './facebook-login.component.html',
  styleUrls: ['./facebook-login.component.scss']
})
export class FacebookLoginComponent {

  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean;
  facebookSettings: facebookSettings;
  url: string = null;

  appURL: string;

  launchFbLogin() {

    console.log(this.facebookSettings);



    this.authWindow = window.open(this.url,null,'width=600,height=400');    
  }

  constructor(private userService: UserService, private router: Router, configService: ConfigurationService) {
    this.isRequesting = true;
    this.appURL = configService.getAppURL();

   configService.getFacebookSettings()
     .subscribe((facebookSettings: facebookSettings) => {
       this.facebookSettings = facebookSettings;
       this.url = `https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=${this.facebookSettings.appId}&display=popup&redirect_uri=${this.appURL}/facebook-auth.html&scope=email`;
       this.isRequesting = false;
     },
        error => {
          //this.notificationService.printErrorMessage(error);
        });

    if (window.addEventListener) {
      window.addEventListener("message", this.handleMessage.bind(this), false);
    } else {
       (<any>window).attachEvent("onmessage", this.handleMessage.bind(this));
    } 
  } 

  handleMessage(event: Event) {
    const message = event as MessageEvent;
    // Only trust messages from the below origin.
    if (message.origin !== this.appURL) return;

    this.authWindow.close();

    const result = JSON.parse(message.data);
    if (!result.status)
    {
      this.failed = true;
      this.error = result.error;
      this.errorDescription = result.errorDescription;
    }
    else
    {
      this.failed = false;
      this.isRequesting = true;

      this.userService.facebookLogin(result.accessToken)
        .finally(() => this.isRequesting = false)
        .subscribe(
        result => {
          if (result) {
            this.router.navigate(['/dashboard/home']);
          }
        },
        error => {
          this.failed = true;
          this.error = error;
        });      
    }
  }
}
