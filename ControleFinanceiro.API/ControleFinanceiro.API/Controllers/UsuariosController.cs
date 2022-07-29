using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Validacoes.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _usuarioRepositorio.PegarPeloId(id);
            try
            {
                if (usuario == null)
                {
                    return NotFound();
                }

                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("SalvarFoto")]
        public async Task<IActionResult> SalvarFoto()
        {
            var foto = Request.Form.Files[0];
            byte[] b;

            using (var openReadStream = foto.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await openReadStream.CopyToAsync(memoryStream);
                    b = memoryStream.ToArray();
                }
            }

            return Ok(new
            {
                foto = b
            }); 
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult usuarioCriado;
                string funcaoUsuario;

                Usuario usuario = new Usuario
                {
                    UserName = model.NomeUsuario,
                    Email = model.Email,
                    PasswordHash = model.Senha,
                    CPF = model.CPF,
                    Profissao = model.Profissao,
                    Foto = model.Foto
                };

                if(await _usuarioRepositorio.PegarQuantidadeUsuariosRegistrados() > 0)
                {
                    funcaoUsuario = "Usuario";
                }
                else
                {
                    funcaoUsuario = "Administrador";
                }

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if(usuarioCriado.Succeeded)
                {
                    await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, funcaoUsuario);
                    await _usuarioRepositorio.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        emailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id
                    }) ;
                }
                else 
                {
                    return BadRequest(model);
                }
            }

            return BadRequest(model);
        }
    }
}
