using RC.Identity.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseApiConfiguration(app.Environment);

app.Run();
