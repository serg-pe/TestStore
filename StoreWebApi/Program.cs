using MediatR;
using Infrastructure;
using Infrastructure.Persistance;
using System.Reflection;
using Application.Common.Mapping;
using Application.Interfaces;
using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", builder =>
    {
        // builder.WithOrigins("http://localhost:8080");
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("Jwt")["Issuer"],
            ValidAudience = builder.Configuration.GetSection("Jwt")["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["Secret"])),
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
