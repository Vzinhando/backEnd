// Adicionando os 'usings' que faltavam para Repositories e Services
using BackEndDemoday.Data;
using BackEndDemoday.Services;
// Usings para Autentica��o e EF Core
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configura��o do Banco de Dados para Railway ---
// O Railway nos d� a URL do banco em uma vari�vel de ambiente.
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// Se a vari�vel n�o existir (rodando localmente), pegamos do appsettings.json
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Trocamos UseSqlite por UseNpgsql para usar PostgreSQL
builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- 2. Inje��o de Depend�ncia (Correto!) ---
builder.Services.AddControllers();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// --- 3. Configura��o de Autentica��o JWT (Correto!) ---
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
        // Lembre-se de configurar estas vari�veis no Railway
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();


// --- 4. Configura��o do Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- 5. Configura��o da Porta para o Railway ---
// O Railway informa a porta na vari�vel de ambiente 'PORT'
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.WebHost.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));


// --- Constru��o da Aplica��o ---
var app = builder.Build();


// --- Pipeline de Middlewares ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// A ordem aqui � crucial e est� correta:
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();