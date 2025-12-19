using WorkFinder.Api;
using WorkFinder.ServiceContracts;
using WorkFinder.Services;
using WorkFinder.Services.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppServices(builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Job.ReadJob", policy =>
        policy.RequireClaim("Permissions", "Job.ActiveJobs", "Job.AvailableJobs"));

var app = builder.Build();

//Seeding Data
using (var scope = app.Services.CreateScope())
{
    //roles
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    await roleService.SeedRolesAsync();

    //modules
    var moduleService = scope.ServiceProvider.GetRequiredService<IModuleService>();
    await moduleService.SeedModulesAsync();

    //permissions
    var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
    await permissionService.SeedPermissionsAsync();

    //rolePermissions
    await roleService.SeedRolePermissionsAsync();

    //skills
    var skillService = scope.ServiceProvider.GetRequiredService<ISkillService>();
    await skillService.SeedSkillsAsync();

    //industries
    var industryService = scope.ServiceProvider.GetRequiredService<IIndustryService>();
    await industryService.SeedIndustriesAsync();

    //qualifications
    var qualificationService = scope.ServiceProvider.GetRequiredService<IQualificationService>();
    await qualificationService.SeedQualficationAsync();

    //countries
    var countries = scope.ServiceProvider.GetRequiredService<ICountryService>();
    await countries.SeedCountriesAsync();

    //cities
    var cities = scope.ServiceProvider.GetRequiredService<ICityService>();
    await cities.SeedCitiesAsync();
}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swg =>
    {
        swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Work Finder API V1");
        swg.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();  //for strict https redirection
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
        ctx.Context.Response.Headers.Append("Access-Control-Expose-Headers", "Content-Disposition");
    }
});      // for serving static files
app.UseRouting();         // for routing

app.UseCors("AllowAll");

app.UseAuthentication(); // for authentication
app.UseAuthorization(); // for authorization


app.MapControllers();  // for execution of endpoints

app.MapHub<ChatHub>("/chatHub"); // maps SignalR Hub

app.Run();
