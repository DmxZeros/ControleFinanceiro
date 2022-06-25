import { Component, OnInit } from '@angular/core';
import { Tipo } from './../../../models/Tipo';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TiposService } from 'src/app/services/tipos.service';
import { CategoriasService } from 'src/app/services/categorias.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-nova-categoria',
  templateUrl: './nova-categoria.component.html',
  styleUrls: ['../listagem-categorias/listagem-categorias.component.css']
})
export class NovaCategoriaComponent implements OnInit {

  formulario: any;
  tipos: Tipo[];
  erros: string[];

  constructor(
    private tipoService: TiposService,
    private categoriaService: CategoriasService,
    private route: Router,
    private snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.tipoService.PegarTodos().subscribe(resultado => {
      this.tipos = resultado;
      this.erros = [];

      console.log(resultado);
    });

    this.formulario = new FormGroup({
      nome: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      icone: new FormControl(null, [Validators.required, Validators.maxLength(15)]),
      tipoId: new FormControl(null, [Validators.required])
    });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  EnviarFormulario(): void {
    const categoria = this.formulario.value;
    this.erros = []; //evita exibir duplicacoes

    this.categoriaService.NovaCategoria(categoria).subscribe((resultado) => {
      this.route.navigate(['categorias/listagemcategorias']);

      this.snackBar.open(resultado.mensagem, null, {
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
    this.route.navigate(['categorias/listagemcategorias'])
  }

}
