using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class MesRepositorio: RepositorioGenerico<Mes>, IMesRepositorio
    {
        private readonly DbContexto _contexto;

        public MesRepositorio(DbContexto contexto): base(contexto)
        {
            _contexto = contexto;
        }

        public new IQueryable<Mes> PegarTodos()
        {
            try
            {
                return _contexto.Meses.OrderBy(m => m.MesId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
