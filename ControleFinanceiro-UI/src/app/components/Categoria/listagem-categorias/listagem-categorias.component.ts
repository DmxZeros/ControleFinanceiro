import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CategoriasService } from 'src/app/services/categorias.service';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map} from 'rxjs/operators';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-listagem-categorias',
  templateUrl: './listagem-categorias.component.html',
  styleUrls: ['./listagem-categorias.component.css']
})
export class ListagemCategoriasComponent implements OnInit {

  categorias = new MatTableDataSource<any>();
  displayColumns: string[];
  autoCompleteInput = new FormControl();
  opcoesCategoria: string[] = [];
  nomesCategorias: Observable<string[]>;

  @ViewChild (MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild (MatSort, {static: true})
  sort: MatSort;

  constructor(private categoriaService: CategoriasService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.categoriaService.PegarTodos().subscribe(resultado => {
      resultado.forEach((categoria) => {
        this.opcoesCategoria.push(categoria.nome);
      });
      //this.categorias.data = resultado;
      this.categorias.data = resultado;
      this.categorias.paginator = this.paginator;
      this.categorias.sort = this.sort;
    });

    this.displayColumns = this.ExibirColunas();

    this.nomesCategorias = this.autoCompleteInput.valueChanges.pipe(startWith(''), map(nome => this.FiltrarNomes(nome)));
  }

  ExibirColunas(): string[]{
    return ['nome', 'icone', 'tipo', 'acoes'];
  }

  AbrirDialog(categoriaId, nome): void {
    this.dialog.open(DialogExclusaoCategoriasComponent, {
      data:{
        categoriaId: categoriaId,
        nome: nome
      },
    })
    .afterClosed()
    .subscribe((resultado) => {
      if(resultado === true) {
        this.categoriaService.PegarTodos().subscribe((dados) => {
          this.categorias.data = dados;
        });

        this.displayColumns = this.ExibirColunas();
      }
    });
  }

  FiltrarNomes(nome: string): string[] {
    if(nome.trim().length >= 3) {
      this.categoriaService
        .FiltrarCategorias(nome.toLocaleLowerCase())
        .subscribe((resultado) => {
          this.categorias.data =resultado;
        });
    }
    else{
      if(nome === ''){
        this.categoriaService.PegarTodos().subscribe((resultado) => {
          this.categorias.data = resultado;
        });
      }
    }

    return this.opcoesCategoria.filter((categoria) =>
      categoria.toLocaleLowerCase().includes(nome.toLocaleLowerCase())
    );
  }

}

@Component({
  selector:'app-dialog-exclusao-categorias',
  templateUrl: 'dialog-exclusao-categorias.html',
})
export class DialogExclusaoCategoriasComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public dados: any,
    private categoriaService: CategoriasService){}
    private snackBar: MatSnackBar

  ExcluirCategoria(categoriaId):void {
    this.categoriaService.ExcluirCategoria(categoriaId).subscribe(resultado => {
      this.snackBar.open(resultado.mensagem, null, {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    });
  }
}
