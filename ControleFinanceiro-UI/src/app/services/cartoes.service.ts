import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cartao } from '../models/Cartao';

const HttpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem('PegarTokenUsuarioLogado')}`
  })
}

@Injectable({
  providedIn: 'root'
})
export class CartoesService {
  url = 'api/Cartoes';

  constructor(private http:HttpClient) { }

  PegarCartaoPeloId(cartaoId: number): Observable<Cartao>{
    const apiUrl = `${this.url}/${cartaoId}`;
    return this.http.get<Cartao>(apiUrl);
  }

  PegarCartoesPeloUsuarioId(usuarioId: string): Observable<Cartao[]>{
    const apiUrl = `${this.url}/PegarCartoesPeloUsuarioId/${usuarioId}`;
    return this.http.get<Cartao[]>(apiUrl);
  }

  NovoCartao(cartao: Cartao): Observable<any>{
    return this.http.post<Cartao>(this.url, cartao, HttpOptions);
  }

  AtualizarCartao(cartaoId:number, cartao: Cartao): Observable<any>{
    const apiUrl = `${this.url}/${cartaoId}`
    return this.http.put<Cartao>(apiUrl, cartao, HttpOptions);
  }

  ExcluirCartao(cartaoId: number): Observable<any>{
    const apiUrl = `${this.url}/${cartaoId}`;
    return this.http.delete<number>(apiUrl, HttpOptions);
  }

  FiltarCartoes(numeroCartao: string): Observable<Cartao[]>{
    const apiUrl = `${this.url}/FiltrarCartoes/${numeroCartao}`;
    return this.http.get<Cartao[]>(apiUrl);
  }
}
