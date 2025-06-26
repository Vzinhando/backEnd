// Importa��es necess�rias
using BackEndDemoday.Data; // Certifique-se que o namespace do seu DbContext est� correto
using BackEndDemoday.Services; // Certifique-se que os namespaces dos seus servi�os est�o corretos
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- MELHORIA: Pol�ticas de CORS espec�ficas para cada ambiente ---
builder.Services.AddCors(options =>
{
    // Pol�tica para desenvolvimento: permite tudo para facilitar os testes locais.
    options.AddPolicy("DevelopmentPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });

    // Pol�tica para produ��o: mais restritiva, permite apenas o seu front-end.
    // Troque "https://seu-frontend.com" pela URL real do seu aplicativo cliente.
    options.AddPolicy("ProductionPolicy",
        policy =>
        {
            policy.WithOrigins("https://seu-frontend.com")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


// --- CONFIGURA��O DO BANCO DE DADOS (L�gica mantida, � excelente) ---
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


// --- INJE��O DE DEPEND�NCIA ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// --- CONFIGURA��O DE AUTENTICA��O JWT ---

// MELHORIA: Valida a exist�ncia da chave JWT na inicializa��o.
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    // Lan�a uma exce��o clara se a chave n�o estiver configurada.
    // Isso evita erros obscuros durante a execu��o.
    throw new InvalidOperationException("A chave do JWT (Jwt:Key) n�o est� configurada nas vari�veis de ambiente.");
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)) // Usa a vari�vel j� validada
    };
});
builder.Services.AddAuthorization();


// --- SWAGGER E OUTROS SERVI�OS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// --- APLICA��O AUTOM�TICA DE MIGRATIONS ---
// Esta fun��o auxiliar torna o c�digo de inicializa��o mais limpo
await ApplyMigrationsAsync(app.Services);


// --- CONFIGURA��O DO PIPELINE HTTP ---

// MELHORIA: Habilita o Swagger apenas em ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Usa a pol�tica de CORS de desenvolvimento
    app.UseCors("DevelopmentPolicy");
}
else
{
    // Em produ��o, usa a pol�tica de CORS restritiva.
    app.UseCors("ProductionPolicy");
}

app.UseHttpsRedirection();

// A ordem aqui � importante: Autentica��o primeiro, depois Autoriza��o.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// --- FUN��O AUXILIAR PARA MIGRATIONS ---
static async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SeuDbContext>(); // <-- Lembre-se de usar o nome do seu DbContext
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Iniciando aplica��o de migrations do banco de dados...");
            // MELHORIA: Usa o m�todo ass�ncrono para n�o bloquear o startup.
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations aplicadas com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Ocorreu um erro cr�tico ao aplicar as migrations.");
            // Opcional: Desligar a aplica��o se as migrations falharem, pois pode ser um estado irrecuper�vel.
            // Environment.Exit(1);
        }
    }
}