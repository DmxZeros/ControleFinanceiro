import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Categoria } from 'src/app/models/Categoria';
import { Tipo } from 'src/app/models/Tipo';
import { CategoriasService } from 'src/app/services/categorias.service';
import { TiposService } from 'src/app/services/tipos.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-atualizar-categoria',
  templateUrl: './atualizar-categoria.component.html',
  styleUrls: ['../listagem-categorias/listagem-categorias.component.css']
})
export class AtualizarCategoriaComponent implements OnInit {

  categoria: Observable<Categoria>
  tipos: Tipo[];
  formulario: any;
  nomeCategoria: string;
  categoriaId: number;
  erros: string[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private tipoService: TiposService,
    private categoriaService: CategoriasService,
    private snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {

    this.erros = [];

    this.categoriaId = this.route.snapshot.params.id;

    this.tipoService.PegarTodos().subscribe(resultado => {
      this.tipos = resultado;
    });

    this.categoriaService.PegarCategoriaPeloId(this.categoriaId).subscribe(resultado => {
      this.nomeCategoria = resultado.nome;
      this.formulario = new FormGroup({
        categoriaId: new FormControl(resultado.categoriaId),
        nome: new FormControl(resultado.nome, [Validators.required, Validators.maxLength(50)]),
        icone: new FormControl(resultado.icone, [Validators.required, Validators.maxLength(15)]),
        tipoId: new FormControl(resultado.tipoId, [Validators.required])
      });
    });
  }

  get propriedade() {
    return this.formulario.controls;
  }

  EnviarFormulario(): void {
    const categoria = this.formulario.value;
    this.erros = []; //evita exibir duplicacoes

    this.categoriaService.AualizarCategoria(this.categoriaId, categoria).subscribe((resultado) =>{
      this.router.navigate(['categorias/listagemcategorias']);

      this.snackBar.open(resultado.mensagem, '', {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    },
    (err) => {
      if(err.status === 400) {
        for(const campo in err.error.errors)
        {
          this.erros.push(err.error.errors[campo]);
        }
      }
    });
  }

  VoltarListagem(): void {
    this.router.navigate(['categorias/listagemcategorias'])
  }

}
