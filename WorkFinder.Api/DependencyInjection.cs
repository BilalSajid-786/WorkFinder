using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.Repositories.Repositories;
using WorkFinder.RepositoryContracts;
using WorkFinder.ServiceContracts;
using WorkFinder.Services;
using WorkFinder.Services.Mappers;

namespace WorkFinder.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Api Controllers
            services.AddControllers();

            //Add swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Add Dapper DbContext
            services.AddScoped<DapperDbContext>();

            //Add AutoMapper for entities to dto and dto to entities
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            //Add Authentication Scheme with Jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            //Add Authorization with policies
            services.AddAuthorization(options =>
            {
                foreach (var permission in SystemPermissions.GetAllPermissions())
                {
                    options.AddPolicy(permission.Action, policy =>
                    {
                        policy.RequireClaim("Permissions", permission.Action);
                    });
                }
            });

            //CORS policy
            services.AddCors(options =>
            {

            });

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISkillRepository,SkillRepository>();
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<IApplicantRepository,ApplicantRepository>();
            services.AddTransient<IIndustryRepository,IndustryRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();

            //Services
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ISkillService,SkillService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IApplicantService,ApplicantService>();
            services.AddTransient<IIndustryService,IndustryService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IModuleService,ModuleService>();

            return services;
        }
    }
}
