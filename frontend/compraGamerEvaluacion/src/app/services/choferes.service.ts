import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface ChoferReadDto {
  id: number;
  dni: string;
  nombre: string;
  microId: number | null;
}

export interface ChoferCreateDto {
  dni: string;
  nombre: string;
  microId: number | null;
}

export interface ChoferUpdateDto {
  dni: string;
  nombre: string;
  microId: number | null;
}

@Injectable({
  providedIn: 'root'
})
export class ChoferesService {

  private baseUrl = `${environment.apiUrl}/choferes`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<ChoferReadDto[]> {
    return this.http.get<ChoferReadDto[]>(this.baseUrl);
  }

  getById(id: number): Observable<ChoferReadDto> {
    return this.http.get<ChoferReadDto>(`${this.baseUrl}/${id}`);
  }

  create(dto: ChoferCreateDto): Observable<ChoferReadDto> {
    return this.http.post<ChoferReadDto>(this.baseUrl, dto);
  }

  update(id: number, dto: ChoferUpdateDto): Observable<ChoferReadDto> {
    return this.http.put<ChoferReadDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  assign(id: number, microId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/${id}/assign/${microId}`, {});
  }

  getByMicro(microId: number): Observable<ChoferReadDto[]> {
  return this.http.get<ChoferReadDto[]>(`${environment.apiUrl}/choferes/micro/${microId}`);
}
}
