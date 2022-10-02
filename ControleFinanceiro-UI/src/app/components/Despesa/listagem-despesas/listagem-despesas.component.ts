import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { map, startWith } from 'rxjs/operators';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DespesasService } from 'src/app/services/despesas.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-listagem-despesas',
  templateUrl: './listagem-despesas.component.html',
  styleUrls: ['./listagem-despesas.component.css']
})
export class ListagemDespesasComponent implements OnInit {

  despesas = new MatTableDataSource<any>();
  displayColumns: string[];
  usuarioId: string = localStorage.getItem('UsuarioId') as string;
  autoCompleteInput = new FormControl();
  opcoesCategorias: string[] = [];
  nomeCategorias: Observable<string[]>;

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  sort: MatSort;


  constructor(
    private desepesasService: DespesasService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.desepesasService.PegarDespesasPeloUsuarioId(this.usuarioId).subscribe((resultado) =>{
      resultado.forEach((despesa) => {
        this.opcoesCategorias.push(despesa.categoria.nome);
      });
      this.despesas.data = resultado;
      this.despesas.paginator = this.paginator;
      this.despesas.sort = this.sort;
    });

    this.displayColumns = this.ExibirColunas();

    this.nomeCategorias = this.autoCompleteInput.valueChanges.pipe(startWith(''), map((nome) => this.FiltrarCategorias(nome)));
  }

  ExibirColunas(): string[]{
    return ['numero', 'descricao', 'categoria', 'valor', 'data', 'acoes'];
  }

  FiltrarCategorias(nomeCategoria: string): string[] {
    if(nomeCategoria.trim().length >= 4){
      this.desepesasService.FiltrarDespesas(nomeCategoria.toLocaleLowerCase()).subscribe(resultado => {
        this.despesas.data = resultado;
      });
    }
    else {
      if(nomeCategoria === ''){
        this.desepesasService.PegarDespesasPeloUsuarioId(this.usuarioId).subscribe(resultado => {
          this.despesas.data = resultado;
        });
      }
    }

    return this.opcoesCategorias.filter(despesa => despesa.toLocaleLowerCase().includes(nomeCategoria.toLocaleLowerCase()));
  }

  AbrirDialog(despesaId: number, valor: number): void {
    this.dialog
    .open(DialogExclusaoDespesasComponent, {
      data: {
        despesaId: despesaId,
        valor: valor
      }
    })
    .afterClosed()
    .subscribe((resultado) => {
      if(resultado === true) {
        this.desepesasService.PegarDespesasPeloUsuarioId(this.usuarioId).subscribe((registros) => {
          this.despesas.data = registros;
          this.despesas.paginator = this.paginator;
        });
        this.displayColumns = this.ExibirColunas();
      }
    })
  }
}

  @Component({
    selector: 'app-dialog-exclusao-despesas',
    templateUrl: 'dialog-exclusao-despesas.html',
  })
  export class DialogExclusaoDespesasComponent {
    constructor(
      @Inject(MAT_DIALOG_DATA) public data: any,
      private despesasService: DespesasService,
      private snackBar: MatSnackBar
    ) {}

    ExcluirDespesa(despesaId: number): void {
      this.despesasService.ExcluirDespesa(despesaId).subscribe((resultado) => {
        this.snackBar.open(resultado.mensagem, '', {
          duration: 2000,
          horizontalPosition: 'right',
          verticalPosition: 'top',
        });
      });
    }
  }
