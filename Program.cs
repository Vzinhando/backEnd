// Importações necessárias
using Microsoft.AspNetCore.HttpOverrides; // <<< ADICIONE ESTA IMPORTAÇÃO
using BackEndDemoday.Data;
using BackEndDemoday.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- MELHORIA: Políticas de CORS específicas para cada ambiente ---
// (código mantido, está correto)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });

    options.AddPolicy("ProductionPolicy",
        policy =>
        {
            var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL");
            if (!string.IsNullOrEmpty(frontendUrl))
            {
                policy.WithOrigins(frontendUrl)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            }
        });
});

// --- CONFIGURAÇÃO DO BANCO DE DADOS (Lógica mantida, está correta) ---
string connectionString;
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var user = userInfo[0];
    var password = userInfo[1];
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={user};Password={password};SslMode=Require;Trust Server Certificate=true;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseNpgsql(connectionString));

// (Resto do código mantido, está correto)
// --- INJEÇÃO DE DEPENDÊNCIA ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// --- CONFIGURAÇÃO DE AUTENTICAÇÃO JWT ---
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("A chave do JWT (Jwt:Key) não está configurada nas variáveis de ambiente.");
}

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddAuthorization();

// --- SWAGGER E OUTROS SERVIÇOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// <<< NOVA CONFIGURAÇÃO ADICIONADA AQUI >>>
// Configura os Forwarded Headers para funcionar atrás do proxy da Railway
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// --- APLICAÇÃO AUTOMÁTICA DE MIGRATIONS ---
await ApplyMigrationsAsync(app.Services);

// --- CONFIGURAÇÃO DO PIPELINE HTTP ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentPolicy");
}
else
{
    app.UseCors("ProductionPolicy");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// --- FUNÇÃO AUXILIAR PARA MIGRATIONS (mantida, está correta) ---
static async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SeuDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            logger.LogInformation("Iniciando aplicação de migrations do banco de dados...");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Ocorreu um erro crítico ao aplicar as migrations.");
        }
    }
}