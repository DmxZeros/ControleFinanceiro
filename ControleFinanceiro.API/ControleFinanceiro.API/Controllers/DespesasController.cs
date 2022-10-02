using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repositorios;
using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesaRepositorio _despesaRespositorio;

        public DespesasController(IDespesaRepositorio despesaRespositorio)
        {
            _despesaRespositorio = despesaRespositorio;
        }

        
        [HttpGet("PegarDespesasPeloUsuarioId/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Despesa>>> PegarDespesasPeloUsuarioId(string usuarioId)
        {
            return await _despesaRespositorio.PegarDespesasPeloUsuarioId(usuarioId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> GetDespesa(int id)
        {
            Despesa despesa = await _despesaRespositorio.PegarPeloId(id);

            if(despesa == null)
            {
                return NotFound();
            }

            return despesa;
        }

        [HttpPost]
        public async Task<ActionResult<Despesa>> PostDespesa(Despesa despesa)
        {
            if(ModelState.IsValid)
            {
                await _despesaRespositorio.Inserir(despesa);

                return Ok(new { 
                    mensagem = $"Despesa no valor de R$ {despesa.Valor} criada com sucesso"
                });
            }

            return BadRequest(despesa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Despesa>> PutDespesa (int id, Despesa despesa)
        {
            if(id != despesa.DespesaId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _despesaRespositorio.Atualizar(despesa);

                return Ok(new
                {
                    mensagem = $"Despesa no valor de R$ {despesa.Valor} atualizada com sucesso"
                });
            }

            return BadRequest(despesa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDespesa(int id)
        {
            Despesa despesa = await _despesaRespositorio.PegarPeloId(id);

            if(despesa == null)
            {
                return NotFound();
            }

            await _despesaRespositorio.Excluir(despesa);

            return Ok(new
            {
                mensagem = $"Despesa no valor de R$ {despesa.Valor} excluída com sucesso"
            });
        }

        [HttpGet("FiltrarDespesas/{nomeCategoria}")]
        public async Task<IEnumerable<Despesa>> FiltrarDespesas(string nomeCategoria)
        {
            return await _despesaRespositorio.FiltrarDespesas(nomeCategoria).ToListAsync();
        }
    }
}
