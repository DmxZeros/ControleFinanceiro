using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleFinanceiro.DAL.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        private readonly DbContexto _contexto;

        public RepositorioGenerico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Atualizar(TEntity entity)
        {
            try 
            {
                var registro = _contexto.Set<TEntity>().Update(entity);
                registro.State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(string id)
        {
            try
            {
                var registro = await PegarPeloId(id);
                _contexto.Set<TEntity>().Remove(registro);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var registro = await PegarPeloId(id);
                _contexto.Set<TEntity>().Remove(registro);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                await _contexto.AddAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Inserir(List<TEntity> entity)
        {
            try
            {
                await _contexto.AddRangeAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(int id)
        {
            try
            {
                var registro = await _contexto.Set<TEntity>().FindAsync(id);
                return registro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(string id)
        {
            try
            {
                var registro = await _contexto.Set<TEntity>().FindAsync(id);
                return registro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<TEntity> PegarTodos()
        {
            try
            {
                return _contexto.Set<TEntity>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
