import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  qtdCartoes: number;
  ganhoTotal: number;
  despesaTotal: number;
  saldo: number;
  anoAtual: number = new Date().getFullYear();
  anoInicial: number = this.anoAtual - 10;
  anos: number[];

  usuarioId: string = localStorage.getItem('UsuarioId') as string;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.dashboardService.PegarDadosCardsDashBoard(this.usuarioId).subscribe((resultado) => {
      this.qtdCartoes = resultado.qtdCartoes;
      this.ganhoTotal = resultado.ganhoTotal;
      this.despesaTotal = resultado.despesaTotal;
      this.saldo = resultado.saldo;
    });

    this.anos = this.CarregarAnos(this.anoInicial, this.anoAtual);
  }

  CarregarAnos(anoInicial: number, anoAtual: number): number[]{
    const anos: any =[];

    while (anoInicial <= anoAtual){
      anos.push(anoInicial);
      anoInicial = anoInicial +1;
    }

    return anos;
  }

}
