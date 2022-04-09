using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class CategoriaRepositorio: RepositorioGenerico<Categoria>, ICategoriaRepositorio
    {
        private readonly DbContexto _contexto;

        public CategoriaRepositorio(DbContexto contexto) :base(contexto)
        {
            _contexto = contexto;
        }

        public new IQueryable<Categoria> PegarTodos()
        {
            try
            {
                return _contexto.Categorias.Include(c => c.Tipo);
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public new async Task<Categoria> PegarPeloId(int id)
        {
            try
            {
                var registro = await _contexto.Categorias.Include(c => c.Tipo).FirstOrDefaultAsync(c => c.CategoriaId == id);
                return registro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<Categoria> FiltrarCategorias(string nomeCategoria)
        {
            try
            {
                var filtro = _contexto.Categorias.Include(c => c.Tipo).Where(c => c.Nome.Contains(nomeCategoria));
                return filtro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
