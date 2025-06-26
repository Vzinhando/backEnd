// Importações necessárias
using BackEndDemoday.Data; // Certifique-se que o namespace do seu DbContext está correto
using BackEndDemoday.Services; // Certifique-se que os namespaces dos seus serviços estão corretos
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- MELHORIA: Políticas de CORS específicas para cada ambiente ---
builder.Services.AddCors(options =>
{
    // Política para desenvolvimento: permite tudo para facilitar os testes locais.
    options.AddPolicy("DevelopmentPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });

    // Política para produção: mais restritiva, permite apenas o seu front-end.
    // Troque "https://seu-frontend.com" pela URL real do seu aplicativo cliente.
    options.AddPolicy("ProductionPolicy",
        policy =>
        {
            policy.WithOrigins("https://seu-frontend.com")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


// --- CONFIGURAÇÃO DO BANCO DE DADOS (Lógica mantida, é excelente) ---
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

builder.Services.AddDbContext<SeuDbContext>(options => // <-- Lembre-se de usar o nome do seu DbContext
    options.UseNpgsql(connectionString));


// --- INJEÇÃO DE DEPENDÊNCIA ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// --- CONFIGURAÇÃO DE AUTENTICAÇÃO JWT ---

// MELHORIA: Valida a existência da chave JWT na inicialização.
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    // Lança uma exceção clara se a chave não estiver configurada.
    // Isso evita erros obscuros durante a execução.
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) // Usa a variável já validada
    };
});
builder.Services.AddAuthorization();


// --- SWAGGER E OUTROS SERVIÇOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// --- APLICAÇÃO AUTOMÁTICA DE MIGRATIONS ---
// Esta função auxiliar torna o código de inicialização mais limpo
await ApplyMigrationsAsync(app.Services);


// --- CONFIGURAÇÃO DO PIPELINE HTTP ---

// MELHORIA: Habilita o Swagger apenas em ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Usa a política de CORS de desenvolvimento
    app.UseCors("DevelopmentPolicy");
}
else
{
    // Em produção, usa a política de CORS restritiva.
    app.UseCors("ProductionPolicy");
}

app.UseHttpsRedirection();

// A ordem aqui é importante: Autenticação primeiro, depois Autorização.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// --- FUNÇÃO AUXILIAR PARA MIGRATIONS ---
static async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SeuDbContext>(); // <-- Lembre-se de usar o nome do seu DbContext
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Iniciando aplicação de migrations do banco de dados...");
            // MELHORIA: Usa o método assíncrono para não bloquear o startup.
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Ocorreu um erro crítico ao aplicar as migrations.");
            // Opcional: Desligar a aplicação se as migrations falharem, pois pode ser um estado irrecuperável.
            // Environment.Exit(1);
        }
    }
}