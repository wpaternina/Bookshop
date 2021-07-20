using Api.Interface;
using Api.Middleware;
using Api.Models;
using Api.Persistences;
using Api.Repository;
using Api.Security.Autenticacion;
using Api.Security.Token;
using Api.Services.Paginacion;
using AutoMapper;
using FluentValidation.AspNetCore;
using Interface;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistences;
using Repository;
using Services.Autor;
using System.Text;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Cadena de conexión de SQL Server con EntityFramework
            services.AddDbContext<BookshopContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddOptions();
            //Conexión a SQL Server Con Dapper.
            services.Configure<ConexionSQLServer>(Configuration.GetSection("ConnectionStrings"));

            // Mediator Autor
            services.AddMediatR(typeof(ConsultaAutor.ManejadorAutor).Assembly);

            // Validaciones Login
            services.AddControllers(opt => {
                // Politica para exigir un token cada vez que ejecute un controlador
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Login>());

            // Injection Identity
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<BookshopContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            // Injection Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Esta es mi llave"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // CUALQUIER TIPO DE REQUEST DE CLIENTE TIENE QUE SER VALIDADO ANTES DE EJECUTARSE
                    IssuerSigningKey = key,
                    ValidateAudience = false, // IP QUE PUEDEN ACCEDER AL TOKEN 
                    ValidateIssuer = false // IP QUE PUEDEN RECIBIR EL TOKEN  
                };
            });

            //Se crear una nueva instancia de la clase FabricaConexion cada que es requerido.
            services.AddTransient<IFabricaConexion, FabricaConexion>();

            //Se crea una nueva instancia una sola vez por cada request.
            services.AddScoped<IJwtGenerador, JwtGenerador>();
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddScoped<IAutor, AutorRepositorio>();
            services.AddScoped<IPaginacion, PaginacionRepositorio>();

            //Crear mapeos personalizados de una clase
            services.AddAutoMapper(typeof(ConsultaAutor.ManejadorAutor));

            //Injection Swagger
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bookshop",
                    Version = "v1"
                });
                s.CustomSchemaIds(s => s.FullName);
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErroresMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookshop v1");
            });
        }
    }
}
