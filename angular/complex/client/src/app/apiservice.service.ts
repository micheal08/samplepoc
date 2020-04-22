import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';



@Injectable()
export class ApiService {
    constructor(private http: HttpClient) {
    }


    public getServerData() {
        return this.http.get('/api/v1/simple');
    }
}
