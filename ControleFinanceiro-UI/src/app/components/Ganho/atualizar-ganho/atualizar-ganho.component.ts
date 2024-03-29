import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { Categoria } from 'src/app/models/Categoria';
import { Ganho } from 'src/app/models/Ganho';
import { Mes } from 'src/app/models/Mes';
import { CategoriasService } from 'src/app/services/categorias.service';
import { GanhosService } from 'src/app/services/ganhos.service';
import { MesService } from 'src/app/services/mes.service';

@Component({
  selector: 'app-atualizar-ganho',
  templateUrl: './atualizar-ganho.component.html',
  styleUrls: ['./atualizar-ganho.component.css']
})
export class AtualizarGanhoComponent implements OnInit {

  ganho: Observable<Ganho>;
  ganhoId: number;
  valorGanho: number;
  categorias: Categoria[];
  meses: Mes[];
  formulario: any;
  erros: string[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ganhosService: GanhosService,
    private categoriaService: CategoriasService,
    private mesesService: MesService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.erros = [];
    this.ganhoId = this.route.snapshot.params.id;

    this.categoriaService.FiltrarCategoriasGanhos().subscribe((resultado) => {
      this.categorias = resultado;
    });

    this.mesesService.PegarTodos().subscribe((resultado) => {
      this.meses = resultado;
    });

    this.ganhosService.PegarGanhoPeloId(this.ganhoId).subscribe((resultado) => {
      this.valorGanho = resultado.valor;

      this.formulario = new FormGroup({
        ganhoId: new FormControl(resultado.ganhoId),
        descricao: new FormControl(resultado.descricao, [Validators.required, Validators.minLength(1), Validators.maxLength(50)]),
        categoriaId: new FormControl(resultado.categoriaId, [Validators.required]),
        valor: new FormControl(resultado.valor, [Validators.required]),
        dia: new FormControl(resultado.dia, [Validators.required]),
        mes: new FormControl(resultado.mes, [Validators.required]),
        ano: new FormControl(resultado.ano, [Validators.required]),
        usuarioId: new FormControl(resultado.usuarioId)
      });
    });
  }

  get propriedade(){
    return this.formulario.controls;
  }

  VoltarListagem(): void {
    this.router.navigate(['/ganhos/listagemganhos']);
  }

  EnviarFormulario(): void {
    const ganho = this.formulario.value;

    this.ganhosService.AtualizarGanho(this.ganhoId, ganho).subscribe((resultado) =>{
      this.router.navigate(['/ganhos/listagemganhos']);
      this.snackBar.open(resultado.mensagem, '', {
        duration: 3000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    },
    (err) => {
      if(err.status === 400){
        for(const campo in err.error.errors){
          if(err.error.errors.hasOwnProperty(campo)){
            this.erros.push(err.error.errors[campo]);
          }
        }
      }
    });
  }
}
