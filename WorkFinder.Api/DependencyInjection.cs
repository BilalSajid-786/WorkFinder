using WorkFinder.Repositories.DbContext;
using WorkFinder.Repositories.Repositories;
using WorkFinder.RepositoryContracts;

namespace WorkFinder.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
        {
            //Add Api Controllers
            services.AddControllers();

            //Add swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Add Dapper DbContext
            services.AddScoped<DapperDbContext>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
