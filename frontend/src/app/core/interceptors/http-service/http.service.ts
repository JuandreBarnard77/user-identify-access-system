import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpServiceInterface } from './http.service.interface';

@Injectable({
  providedIn: 'root'
})
export class HttpService implements HttpServiceInterface {
  constructor(private http: HttpClient) { }

  public get<T>(url: string, headers: HttpHeaders, params?: HttpParams | undefined): Observable<T> {
    return this.http.get<T>(url, { headers, params });
  }
  public post<T, U>(url: string, headers: HttpHeaders, body?: U, params?: HttpParams | undefined): Observable<T> {
    return this.http.post<T>(url, body, { headers, params });
  }
  public put<T, U>(url: string, headers: HttpHeaders, body?: U, params?: HttpParams | undefined): Observable<T> {
    return this.http.put<T>(url, body, { headers, params });
  }
  public delete<T, U>(url: string, headers: HttpHeaders, body?: U, params?: HttpParams | undefined): Observable<T> {
    return this.http.delete<T>(url, { headers, params , body});
  }
}
