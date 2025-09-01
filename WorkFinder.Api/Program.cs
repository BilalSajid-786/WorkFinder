using WorkFinder.Api;
using WorkFinder.ServiceContracts;
using WorkFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppServices(builder.Configuration);

var app = builder.Build();

//Seeding roles
using (var scope = app.Services.CreateScope())
{
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    await roleService.SeedRolesAsync();

    var skillService = scope.ServiceProvider.GetRequiredService<ISkillService>();
    await skillService.SeedSkillsAsync();

    var industryService = scope.ServiceProvider.GetRequiredService<IIndustryService>();
    await industryService.SeedIndustriesAsync();
}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swg =>
    {
        swg.SwaggerEndpoint("/swagger/v1/swagger.json", "Wrok Finder API V1");
    });
}

app.UseHttpsRedirection();  //for strict https redirection
app.UseStaticFiles();      // for serving static files
app.UseRouting();         // for routing

app.UseAuthentication(); // for authentication
app.UseAuthorization(); // for authorization


app.MapControllers();  // for execution of endpoints

app.Run();
