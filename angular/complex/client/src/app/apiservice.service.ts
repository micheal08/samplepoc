import { Injectable } from '@angular/core';
import { DataService } from './shared/data.service';
import { HttpClient} from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class ApiService {
    constructor(private http: HttpClient) {
    }


    public getServerData() {
        // return this.service.get('/api/v1/Simple').pipe(map((response: any) => {
        //     return response;
        // }));
        return this.http.get('/api/v1/Simple', {responseType: 'text'});

        // responseType?: 'arraybuffer' | 'blob' | 'json' | 'text'

    }
}
