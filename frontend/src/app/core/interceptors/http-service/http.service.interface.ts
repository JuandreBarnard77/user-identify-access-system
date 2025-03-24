import { HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

export interface HttpServiceInterface {
  get<T>(url: string, headers: HttpHeaders, params?: HttpParams): Observable<T>;
  post<T, U>(url: string, headers: HttpHeaders, body?: U, params?: HttpParams): Observable<T>;
  put<T, U>(url: string, headers: HttpHeaders, body?: U, params?: HttpParams): Observable<T>;
  delete<T>(url: string, headers: HttpHeaders, params?: HttpParams): Observable<T>;
}
