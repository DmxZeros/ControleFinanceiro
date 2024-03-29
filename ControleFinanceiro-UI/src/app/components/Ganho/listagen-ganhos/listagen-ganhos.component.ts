import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { GanhosService } from 'src/app/services/ganhos.service';

@Component({
  selector: 'app-listagen-ganhos',
  templateUrl: './listagen-ganhos.component.html',
  styleUrls: ['./listagen-ganhos.component.css']
})
export class ListagenGanhosComponent implements OnInit {
  ganhos = new MatTableDataSource<any>();
  displayedColumns: string[];
  usuarioId: string = localStorage.getItem('UsuarioId') as string;
  autoCompleteInput = new FormControl();
  opcoesCategorias: string[] = [];
  nomesCategorias: Observable<string[]>;

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  constructor(
    private ganhosService: GanhosService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.ganhosService.PegarGanhosPeloUsuarioId(this.usuarioId).subscribe((resultado) => {
      this.ganhos.data = resultado;
      this.ganhos.paginator = this.paginator;
      this.ganhos.sort = this.sort;
    });

    this.displayedColumns = this.ExibirColunas();
  }

  ExibirColunas(): string[] {
    return['descricao','categoria','valor','data','acoes'];
  }

  FiltrarCategorias(nomesCategoria: string): string[] {
    if(nomesCategoria.trim().length >=4){
      this.ganhosService.FiltrarGanhos(nomesCategoria.toLocaleLowerCase()).subscribe((resultado) => {
        this.ganhos.data = resultado;
      });
    }
    else {
      if(nomesCategoria === ''){
        this.ganhosService.PegarGanhosPeloUsuarioId(this.usuarioId).subscribe((resultado) =>{
          this.ganhos.data = resultado;
        });
      }
    }

    return this.opcoesCategorias.filter((nome) =>
      nome.toLowerCase().includes(nomesCategoria.toLowerCase())
    );
  }

  AbrirDialog(ganhoId: any, valor: any): void {
    this.dialog.open(DialogExclusaoGanhosComponent, {
      data:{
        ganhoId: ganhoId,
        valor: valor
      },
    }).afterClosed().subscribe((resultado) =>{
      if(resultado === true){
        this.ganhosService.PegarGanhosPeloUsuarioId(this.usuarioId).subscribe((registros) =>{
          this.ganhos.data = registros;
          this.ganhos.paginator = this.paginator;
        });
        this.displayedColumns = this.ExibirColunas();
      }
    });
  }
}

@Component({
  selector: 'app-dialog-exclusao-ganhos',
  templateUrl: 'dialog-exclusao-ganhos.html',
})
export class DialogExclusaoGanhosComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private ganhosService: GanhosService,
    private snackBar: MatSnackBar
  ) {}

  ExcluirGanho(ganhoId: number): void {
    this.ganhosService.ExcluirGanho(ganhoId).subscribe((resultado) =>{
      this.snackBar.open(resultado.mensagem, '', {
        duration: 3000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    });
  }
}
