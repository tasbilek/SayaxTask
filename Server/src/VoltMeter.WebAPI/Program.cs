using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VoltMeter.Application;
using VoltMeter.Infrastructure;
using VoltMeter.Infrastructure.Context;
using VoltMeter.WebAPI.Middlewares;
using VoltMeter.WebAPI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseExceptionHandler();
app.RegisterRoutes();

using (var scoped = app.Services.CreateScope())
{
    var srv = scoped.ServiceProvider;
    var context = srv.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
app.Run();
