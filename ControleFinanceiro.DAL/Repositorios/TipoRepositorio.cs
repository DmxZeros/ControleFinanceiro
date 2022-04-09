using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class TipoRepositorio: RepositorioGenerico<Tipo>, ITipoRepositorio
    {
        public TipoRepositorio(DbContexto contexto): base(contexto)
        {

        }
    }
}
