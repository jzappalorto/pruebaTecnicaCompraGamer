import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface MicroReadDto {
  id: number;
  patente: string;
  marcaModelo: string;
}

export interface MicroCreateDto {
  patente: string;
  marcaModelo: string;
}

export interface MicroUpdateDto {
  patente: string;
  marcaModelo: string;
}

@Injectable({
  providedIn: 'root'
})
export class MicrosService {

  private baseUrl = `${environment.apiUrl}/micros`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<MicroReadDto[]> {
    return this.http.get<MicroReadDto[]>(this.baseUrl);
  }

  getById(id: number): Observable<MicroReadDto> {
    return this.http.get<MicroReadDto>(`${this.baseUrl}/${id}`);
  }

  create(dto: MicroCreateDto): Observable<MicroReadDto> {
    return this.http.post<MicroReadDto>(this.baseUrl, dto);
  }

  update(id: number, dto: MicroUpdateDto): Observable<MicroReadDto> {
    return this.http.put<MicroReadDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getChoferByMicro(id: number): Observable<{ nombre: string }> {
  return this.http.get<{ nombre: string }>(`${this.baseUrl}/${id}/chofer`);
}

}
