using ApiDemoday.Data;
using ApiDemoday.Profiles; 
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cloudinaryAccount = new Account(
    builder.Configuration["CLOUDINARY_CLOUD_NAME"],
    builder.Configuration["CLOUDINARY_API_KEY"],
    builder.Configuration["CLOUDINARY_API_SECRET"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") // conexcao com front
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RailwayContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); //conecxao com o banco



builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger(); // subi essas 2 linhas para abrir o swagger, caso nao queira acessar o swagger coloque dentro do if abaixo
    app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () => "API is healthy");

app.Run();
