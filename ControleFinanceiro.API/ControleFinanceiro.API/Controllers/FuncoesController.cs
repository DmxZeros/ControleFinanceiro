using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Validacoes.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoRepositorio _funcaoRespositorio;

        public FuncoesController(IFuncaoRepositorio funcaoRespositorio)
        {
            _funcaoRespositorio = funcaoRespositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
        {
            return await _funcaoRespositorio.PegarTodos().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcao>> GetFuncao(string id)
        {
            var funcao = await _funcaoRespositorio.PegarPeloId(id);

            if (funcao == null)
            {
                return NotFound();
            }

            return funcao;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFuncao(string id, FuncoesViewModel funcoes)
        {
            if (id != funcoes.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Funcao funcao = new Funcao
                {
                    Id = funcoes.Id,
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao
                };

                await _funcaoRespositorio.AtualizarFuncao(funcao);

                return Ok(new
                {
                    mensagem = $"Função {funcao.Name} atualizada com sucesso"
                });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult<Funcao>> PostFuncao(FuncoesViewModel funcoes)
        {
            if (ModelState.IsValid)
            {
                Funcao funcao = new Funcao
                {
                    Name = funcoes.Name,
                    Descricao = funcoes.Descricao
                };

                await _funcaoRespositorio.AdicionarFuncao(funcao);

                return Ok(new
                {
                    mensagem = $"Função {funcao.Name} adicionada com sucesso"
                });
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcao>> DeleteFuncao(string id)
        {
            var funcao = await _funcaoRespositorio.PegarPeloId(id);

            if (funcao ==null)
            {
                return NotFound();
            }

            await _funcaoRespositorio.Excluir(funcao);

            return Ok(new
            {
                mensagem = $"Função {funcao.Name} excluída com sucesso"
            });
        }

    }       
}
