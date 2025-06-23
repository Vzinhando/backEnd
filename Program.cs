// Adicionando os 'usings' que faltavam para Repositories e Services
using BackEndDemoday.Data;
using BackEndDemoday.Services;
// Usings para Autenticação e EF Core
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração do Banco de Dados para Railway ---
// O Railway nos dá a URL do banco em uma variável de ambiente.
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// Se a variável não existir (rodando localmente), pegamos do appsettings.json
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Trocamos UseSqlite por UseNpgsql para usar PostgreSQL
builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- 2. Injeção de Dependência (Correto!) ---
builder.Services.AddControllers();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// --- 3. Configuração de Autenticação JWT (Correto!) ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // Lembre-se de configurar estas variáveis no Railway
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();


// --- 4. Configuração do Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- 5. Configuração da Porta para o Railway ---
// O Railway informa a porta na variável de ambiente 'PORT'
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.WebHost.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));


// --- Construção da Aplicação ---
var app = builder.Build();


// --- Pipeline de Middlewares ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// A ordem aqui é crucial e está correta:
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();