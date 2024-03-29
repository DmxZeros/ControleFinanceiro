import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})

export class AuthGuardService implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('TokenUsuarioLogado');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }

    this.router.navigate(['usuarios/loginusuario']);
    return false;
  }

  VerificarAdministrador(): boolean {
    const token = localStorage.getItem('TokenUsuarioLogado');
    //const tokenUsuario : any = decode(token);

    if(token) {
      const tokenUSuario :any = decode(token);

      if(tokenUSuario.role === 'Administrador')
          return true;
    }

    return false;
  }
}
