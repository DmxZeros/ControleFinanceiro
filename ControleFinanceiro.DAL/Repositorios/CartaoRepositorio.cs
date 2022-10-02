using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class CartaoRepositorio : RepositorioGenerico<Cartao>, ICartaoRepositorio
    {
        private readonly DbContexto _contexto;

        public CartaoRepositorio(DbContexto contexto): base(contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<Cartao> FiltrarCartoes(string numeroCartao)
        {
            try
            {
                return _contexto.Cartoes.Where(c => c.Numero.Contains(numeroCartao));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<Cartao> PegarCartoesPeloUsuarioId(string usuarioId)
        {
            try
            {
                return _contexto.Cartoes.Where(c => c.UsuarioId == usuarioId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> PegarQuantidadCartoesPeloUsuarioId(string usuarioId)
        {
            try
            {
                return await _contexto.Cartoes.CountAsync(c => c.UsuarioId == usuarioId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
