using Sulpak.TestAssigment.Infrastructure;
using Sulpak.TestAssigment.PublicApi;
using Sulpak.TestAssigment.PublicApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.ConfigureVersioning();
builder.ConfigureSwagger();

//init application layer
builder.Services.ConfigureUseCases();

//init infrastructure layer
builder.Services.ConfigureDatabase()
    .ConfigureRepositories();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();