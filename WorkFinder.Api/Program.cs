using WorkFinder.Api;
using WorkFinder.ServiceContracts;
using WorkFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppServices(builder.Configuration);

var app = builder.Build();

//Seeding roles
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
}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swg =>
    {
        swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Work Finder API V1");
    });
}

app.UseHttpsRedirection();  //for strict https redirection
app.UseStaticFiles();      // for serving static files
app.UseRouting();         // for routing

app.UseCors(x => x   
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication(); // for authentication
app.UseAuthorization(); // for authorization


app.MapControllers();  // for execution of endpoints

app.Run();
