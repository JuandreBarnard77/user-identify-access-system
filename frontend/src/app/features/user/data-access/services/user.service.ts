import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { User } from '../dtos/user';
import { HttpService } from '../../../../core/interceptors/http-service/http.service';
import { environment } from '../../../../../environments/environment';
import { UserDto } from '../dtos/user.dto';
import { UserMapper } from '../mapper/user.mapper';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private headers: HttpHeaders;
  private mapper: UserMapper = new UserMapper();
  constructor(private http: HttpService) {
    this.headers = new HttpHeaders({
      'Accept': '*/*',
      'Accept-Language': 'en-GB,en;q=0.7',
      'Connection': 'keep-alive',
    });
  }


  public addUser(object: User): Observable<User> {
    const url = `${environment.apiBaseUrl}/users`;
    return this.http.post(url, this.headers, this.mapper.mapToDto(object));
  }

  public editUser(object: User): Observable<User> { console.log(object)
    const url = `${environment.apiBaseUrl}/users/${object.id}`;
    return this.http.put(url, this.headers, this.mapper.mapToDto(object));
  }

  public deleteUser(objectId: number): Observable<void> {
    const url = `${environment.apiBaseUrl}/users/${objectId}`;
    return this.http.delete(url, this.headers);
  }

  public getAllUsers(): Observable<User[]> {
    const url = `${environment.apiBaseUrl}/users`;
    const response = this.http.get<UserDto[]>(url, this.headers);
    return response.pipe(map(data => this.mapper.mapToModels(data)));
  }

  public getUserById(userId: number): Observable<User> {
    const url = `${environment.apiBaseUrl}/users/${userId}`;
    const response = this.http.get<UserDto>(url, this.headers);
    return (response.pipe(map(data => this.mapper.mapToModel(data))));
  }

  public getUserCount(userId: number): Observable<User> {
    const url = `${environment.apiBaseUrl}/users/count`;
    const response = this.http.get<UserDto>(url, this.headers);
    return (response.pipe(map(data => this.mapper.mapToModel(data))));
  }
}

