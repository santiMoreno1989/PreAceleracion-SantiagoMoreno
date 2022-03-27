﻿using ApiPreAceleracionAlkemy.Entities;
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IPeliculaService, PeliculaService>();
            services.AddScoped<IPersonajeService, PersonajeService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSendGrid(k => k.ApiKey = "...Ingrese SendGrid Key Aqui...");
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ActionFilter>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
