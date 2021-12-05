using Infrastructure;
using Infrastructure.Persistance;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
