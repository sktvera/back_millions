// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Million.Domain.Configuration;        // MongoSettings
using Million.Infrastructure.Database;
using Million.Infrastructure.Repositories;
using Million.Application.Services;

var builder = WebApplication.CreateBuilder(args);



builder.WebHost.ConfigureKestrel(o =>
{
    // hasta 10MB
    o.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
});


// --- 1) Servicios (antes de Build) ---
var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
              ?? new[] { "http://localhost:5173" }; // Vite por defecto

builder.Services.AddCors(o => o.AddPolicy("frontend", p =>
    p.WithOrigins(origins)
     .AllowAnyHeader()
     .AllowAnyMethod()
));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI de Infra/Application
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyService>();

var app = builder.Build();

// --- 2) Middleware (después de Build) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("frontend");

app.MapControllers();

app.Run();
