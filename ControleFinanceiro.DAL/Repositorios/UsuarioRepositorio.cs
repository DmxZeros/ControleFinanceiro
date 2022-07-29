using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositorios
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private readonly DbContexto _contexto;
        private readonly UserManager<Usuario> _gerenciadorsuarios;
        private readonly SignInManager<Usuario> _gerenciadorLogin;

        public UsuarioRepositorio(DbContexto contexto, UserManager<Usuario> gerenciadorsuarios, SignInManager<Usuario> gerenciadorLogin): base(contexto)
        {
            _contexto = contexto;
            _gerenciadorsuarios = gerenciadorsuarios;
            _gerenciadorLogin = gerenciadorLogin;
        }

        public async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
        {
            try
            {
                return await _gerenciadorsuarios.CreateAsync(usuario, senha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task IncluirUsuarioEmFuncao(Usuario usuario, string funcoes)
        {
            try
            {
                await _gerenciadorsuarios.AddToRoleAsync(usuario, funcoes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LogarUsuario(Usuario usuario, bool lembrar)
        {
            try
            {
                await _gerenciadorLogin.SignInAsync(usuario, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> PegarQuantidadeUsuariosRegistrados()
        {
            try
            {
                return await _contexto.Usuarios.CountAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
