using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloAutenticao;
using eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloCompromisso;
using eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloContato;
using eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloDespesa;
using eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloTarefa;
using eAgenda.Webpi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace eAgenda.Webpi
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
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(config =>
            {
                config.AddProfile<TarefaProfile>();
                config.AddProfile<ContatoProfile>();
                config.AddProfile<CompromissoProfile>();
                config.AddProfile<CategoriaProfile>();
                config.AddProfile<DespesaProfile>();
                config.AddProfile<UsuarioProfile>();
            });
            services.AddSingleton((x) => new ConfiguracaoAplicacaoeAgenda().ConnectionStrings);

            services.AddScoped<eAgendaDbContext>();
            services.AddIdentity<Usuario, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<eAgendaDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IContextoPersistencia, eAgendaDbContext>();
            services.AddScoped<IRepositorioTarefa, RepositorioTarefaOrm>();
            services.AddScoped<IRepositorioContato, RepositorioContatoOrm>();
            services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoOrm>();
            services.AddScoped<IRepositorioCategoria, RepositorioCategoriaOrm>();
            services.AddScoped<IRepositorioDespesa, RepositorioDespesaOrm>();

            services.AddTransient<UserManager<Usuario>>();
            services.AddTransient<SignInManager<Usuario>>();

            services.AddTransient<ServicoTarefa>();
            services.AddTransient<ServicoContato>();
            services.AddTransient<ServicoCompromisso>();
            services.AddTransient<ServicoCategoria>();
            services.AddTransient<ServicoDespesa>();
            services.AddTransient<ServicoAutenticacao>();

            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidarViewModelActionFilter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eAgenda.Webpi", Version = "v1" });
            });

            var key = Encoding.ASCII.GetBytes("SegredoSuperSecretoDoeAgenda");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidAudience = "http://localhost",
                    ValidIssuer = "eAgenda"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eAgenda.Webpi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
