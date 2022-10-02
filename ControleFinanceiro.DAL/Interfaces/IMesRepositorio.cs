using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControleFinanceiro.BLL.Models;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IMesRepositorio: IRepositorioGenerico<Mes>
    {
        new IQueryable<Mes> PegarTodos();
    }
}
