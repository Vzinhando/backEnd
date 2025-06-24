using BackEndDemoday.Data;
using BackEndDemoday.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURA��O DO BANCO DE DADOS ---
// Pega a string de conex�o do appsettings.json para desenvolvimento local.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// O Railway injeta a connection string na vari�vel de ambiente DATABASE_URL.
// Verificamos se essa vari�vel existe para usar em produ��o.
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    // Converte a URL do Railway para o formato de connection string do Npgsql.
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var user = userInfo[0];
    var password = userInfo[1];
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={user};Password={password};";
}

// Adiciona o DbContext usando o provedor Npgsql (PostgreSQL).
builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseNpgsql(connectionString));


// --- RESTANTE DAS CONFIGURA��ES ---
builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica as migrations do Entity Framework automaticamente na inicializa��o.
// Isso � �til para ambientes como o Railway.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SeuDbContext>();
    // Use o m�todo `Database.Migrate()` para aplicar as migrations.
    dbContext.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
