using Hangfire;
using Hangfire_project.Interfaces;
using Hangfire_project.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IUserDelayedService, UserDelayedService>();

services.AddControllers();

services.AddHangfire(cnf => 
    cnf.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

services.AddHangfireServer();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("", () => "Hello from main server");

app.Run();
