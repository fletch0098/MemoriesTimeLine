import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { UserService } from '../shared/services/user.service';
import { UserDetails } from '../shared/models/userDetails';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit,OnDestroy {

  isExpanded = false;

  status: boolean;
  subscription: Subscription;

  userDetails: UserDetails;

  constructor(private userService: UserService) {

  }

  logout() {
    this.userService.logout();
  }

  ngOnInit() {
    this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);

    console.log('NavBar - isLoggegIn: ' + this.userService.isLoggedIn());

    if (this.userService.isLoggedIn()) {
      this.userService.getUserDetails()
        .subscribe((userDetails: UserDetails) => {
          console.log('NavBar - userDetails: ' + userDetails);
          this.userDetails = userDetails;
        },
          error => {
            //this.notificationService.printErrorMessage(error);
          });
    }
    

    
  }

  ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
