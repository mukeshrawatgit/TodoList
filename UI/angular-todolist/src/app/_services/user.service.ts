import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

const baseUrl = 'http://localhost:27667/api/todoitem';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

 

  getAll(): Observable<any> {
    return this.http.get(baseUrl + '/Getlist?user=' + environment.username);
  }

 

  create(data): Observable<any> {

    debugger
    return this.http.post(baseUrl + '/CreatetodoItem?user=' + environment.username, data);
  }

  update(data): Observable<any> {
    return this.http.put(baseUrl + '/UpdateItem?user=' + environment.username, data);
  }

  delete(id): Observable<any> {
    return this.http.delete(baseUrl + '/DeleteItem?id=' + id  + '&user=' + environment.username);
  }
}