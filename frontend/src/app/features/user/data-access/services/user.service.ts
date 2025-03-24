import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../dtos/user';
import { HttpService } from '../../../../core/interceptors/http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpService) { }

  getUsers(): Observable<User[]> {
    return of([{ id: 1, name: 'John Doe' }]); // Mock API call
  }

  public add(object: User): Observable<User> {
    const url = `${this.baseUrl}/dbprovider/dbconnections`;
    return this.http.post(url, this.mapper.mapToDto(object));
  }

  public edit(object: User): Observable<User> {
    const url = `${this.baseUrl}/dbprovider/dbconnections`;
    return this.http.put(url, this.mapper.mapToDto(object));
  }

  public delete(object: User): Observable<void> {
    const url = `${this.baseUrl}/dbprovider/dbconnections`;
    return this.http.delete(url, this.mapper.mapToDto(object));
  }

  public getAll(): Observable<User[]> {
    const url = `${this.baseUrl}/dbprovider/dbconnections`;
    const response = this.http.get<RequestResponse<DbConnectionDto>>(url);
    return response.pipe(map(data => this.mapper.mapToModels(data._responseData)));
  }

  public getUserCount() Observable<number> {
    const url = `${this.baseUrl}/dbprovider/dbconnections`;
    const response = this.http.get<RequestResponse<DbConnectionDto>>(url);
    return response.pipe(map(data => this.mapper.mapToModels(data._responseData)));
  }
}

