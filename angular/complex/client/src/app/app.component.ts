import { Component } from '@angular/core';
import {ApiService} from './apiservice.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  serverData = 'To get server data click Get button';

  constructor(private apiService: ApiService) {

  }

  onGetDataFromServer() {
    this.apiService.getServerData();
  }
}
