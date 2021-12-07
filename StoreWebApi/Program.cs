using MediatR;
using Infrastructure;
using Infrastructure.Persistance;
using System.Reflection;
using Application.Common.Mapping;
using Application.Interfaces;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", builder =>
    {
        builder.WithOrigins("http://localhost:8080", "https://localhost:7069");
        /*builder.AllowAnyMethod();*/
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(assemblies =>
{
    assemblies.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    assemblies.AddProfile(new AssemblyMappingProfile(typeof(IStoreDbContext).Assembly));
});

builder.Services.AddApplication();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        DbInitializer.Initialize(dbContext);
    }
    catch (Exception exception)
    {
        app.Logger.LogError(exception, "Database initialization error");
    }
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("Default");

app.UseAuthorization();

app.MapControllers();

app.Run();
