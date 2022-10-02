import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Mes } from '../models/Mes';

@Injectable({
  providedIn: 'root'
})
export class MesService {

  url = 'api/Mes';

  constructor(private http: HttpClient) { }

  PegarTodos(): Observable<Mes[]>
  {
    return this.http.get<Mes[]>(this.url);
  }
}
