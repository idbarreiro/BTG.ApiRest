using BTG.Application.Interfaces;
using BTG.Domain.Entities;
using BTG.Persistence.Context;
using BTG.Persistence.Repository;
using BTG.Persistence.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BTG.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<ApplicationDbContext>();

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<Transaction>), typeof(MyRepositoryAsync<Transaction>));
            #endregion

        }
    }
}
