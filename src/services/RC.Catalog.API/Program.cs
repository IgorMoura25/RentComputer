using RC.Catalog.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.RegisterServices();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerConfiguration();

app.UseRouting();

app.UseApiConfiguration(app.Environment);

app.Run();
