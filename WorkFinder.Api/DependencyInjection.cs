using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WorkFinder.Common;
using WorkFinder.Entities.Entities.SystemSeeding;
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Serialize enums as strings instead of ints
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            //Add swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Get the XML file name (same as assembly)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Include XML comments
                options.IncludeXmlComments(xmlPath);
            });

            //Add Dapper DbContext
            services.AddScoped<DapperDbContext>();

            //Add AutoMapper for entities to dto and dto to entities
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            //SignalR
            services.AddSignalR();

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
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod(); // Works without credentials
                });
            });

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<IApplicantRepository, ApplicantRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IQualificationRepository, QualificationRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            //Services
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IApplicantService, ApplicantService>();
            services.AddTransient<IIndustryService, IndustryService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IQualificationService, QualificationService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IMessageService, MessageService>();


            //SingalR
            services.AddSingleton<UserConnectionManager>();

            return services;
        }
    }
}
