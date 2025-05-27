import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../../environments/environment';
import { IResponse } from '../../shared/interfaces';

const apiUrl = environment.apiUrl;

@Injectable({
    providedIn: 'root',
})

export class ApiService {
    constructor(private http: HttpClient) {}

    get<T>(url: string, params: HttpParams = null): Observable<IResponse<T>> {
        const headers = this.getPublicHeaders();
        return this.http.get<IResponse<T>>(apiUrl + url, { headers, params });
    }

    delete<T>(url: string): Observable<IResponse<T>> {
        const headers = this.getPublicHeaders();
        return this.http.delete<IResponse<T>>(apiUrl + url, { headers });
    }

    post<T>(url: string, body: any): Observable<IResponse<T>> {
        const headers = this.getPublicHeaders();
        return this.http.post<IResponse<T>>(apiUrl + url, body, { headers });
    }

    put<T>(url: string, body: any): Observable<IResponse<T>> {
        const headers = this.getPublicHeaders();
        return this.http.put<IResponse<T>>(apiUrl + url, body, { headers });
    }

    private getPublicHeaders() {
      return {
        'Content-Type': 'application/json',
      };
    }
}
