import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Chico {
  id: number;
  dni: string;
  nombre: string;
  microId: number;
}

export interface ChicoCreate {
  dni: string;
  nombre: string;
  microId: number;
}

@Injectable({ providedIn: 'root' })
export class ChicosService {
  private api = 'http://localhost:8080/api/chicos';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Chico[]> {
    return this.http.get<Chico[]>(this.api);
  }

  getById(id: number): Observable<Chico> {
    return this.http.get<Chico>(`${this.api}/${id}`);
  }

  create(dto: ChicoCreate): Observable<Chico> {
    return this.http.post<Chico>(this.api, dto);
  }

  update(id: number, dto: ChicoCreate): Observable<Chico> {
    return this.http.put<Chico>(`${this.api}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }

  getByMicro(microId: number): Observable<Chico[]> {
    return this.http.get<Chico[]>(`${this.api}/micro/${microId}`);
  }

  assign(id: number, microId: number): Observable<void> {
    return this.http.post<void>(`${this.api}/${id}/assign/${microId}`, {});
  }
}
