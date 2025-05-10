using EnsayoCrudNetAngular.Application.Interface;
using EnsayoCrudNetAngular.Application.Services;
using EnsayoCrudNetAngular.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Application layer
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddInfrastructure(builder.Configuration);

// Swagger, Controllers, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Mostrar Swagger en entorno de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
