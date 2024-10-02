using BTG.Application.Behaviors;
using BTG.Application.Interfaces;
using BTG.Application.Services;
using BTG.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTG.Application
{
    public static class ServiceExtensions
    {
        /**
         * registra todos los servicios de la capa de aplicación
         * el método AddAutoMapper registra todos los perfiles de mapeo de la capa de aplicación
         * el método AddValidatorsFromAssembly registra todos los validadores de la capa de aplicación
         * el método AddMediatR registra todos los controladores de la capa de aplicación
         * el addtransient registra el comportamiento de validación
         */
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationsBehavior<,>));

            #region Services
            services.AddTransient(typeof(INotificationTransactionServiceAsync), typeof(NotificationTransactionService));
            #endregion
        }
    }
}
