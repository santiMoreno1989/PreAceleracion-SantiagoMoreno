using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Filter.PersonajesFilter;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.Services;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services) 
        {
            services.AddScoped<IPersonajeRepository, PersonajeRepository>();
            services.AddScoped<IPeliculaRepository, PeliculaRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddSingleton<IMailService, MailService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPersonajeService, PersonajeService>();
            services.AddScoped<IPeliculaService, PeliculaService>();
            services.AddSendGrid(k => k.ApiKey = "...Ingrese SendGrid Key Aqui...");
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ActionFilter>();

            return services;
        }
    }
}
