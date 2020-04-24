import { Component } from '@angular/core';
import { ApiService } from './apiservice.service';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  serverData: any;


  constructor(private apiService: ApiService) {
    this.serverData = 'To get server data click Get button';
  }

  onGetDataFromServer() {
    this.apiService.getServerData().subscribe(res  => {
        console.log('Response - ' + res);
        this.serverData = res;
      }, error => {
        console.error(error);
      });
  }
}
