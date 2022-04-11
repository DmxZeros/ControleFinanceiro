using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.DAL.Contexto;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repositorios;
using FluentValidation;
using ControleFinanceiro.API.Validacoes;
using FluentValidation.AspNetCore;

namespace ControleFinanceiro.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("Conexao")));

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores < DbContexto >();

            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<ITipoRepositorio, TipoRepositorio>();
            services.AddScoped<IFuncaoRepositorio, FuncaoRepositorio>();

            //fluente api
            services.AddTransient<IValidator<Categoria>, CategoriaValidator>();

            services.AddCors();

            services.AddSpaStaticFiles(diretorio =>
            {
                diretorio.RootPath = "ControleFinanceiro-UI";
            });

            services.AddControllers()
                .AddFluentValidation()

                .AddJsonOptions(opcoes =>
                {
                    opcoes.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddNewtonsoftJson(opcoes => {
                    opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI");

                if(env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            });
        }
    }
}
