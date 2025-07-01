using ApiDemoday.Data;
using Microsoft.EntityFrameworkCore;
using ApiDemoday.Profiles; 
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RailwayContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); //conecxao com o banco

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

    app.UseSwagger(); // subi essas 2 linhas para abrir o swagger, caso nao queira acessar o swagger coloque dentro do if abaixo
    app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
