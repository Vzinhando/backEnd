using BackEndDemoday.Data;
using BackEndDemoday.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// --- CONFIGURA��O DO BANCO DE DADOS (VERS�O FINAL) ---
string connectionString;
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(databaseUrl))
{
    // L�gica para converter a DATABASE_URL do Railway para a connection string do Npgsql.
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var user = userInfo[0];
    var password = userInfo[1];

    // Adiciona SslMode=Require e Trust Server Certificate=true, que � crucial para conex�es em nuvem como o Railway.
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={user};Password={password};SslMode=Require;Trust Server Certificate=true;";
}
else
{
    // Usa a connection string local do appsettings.json se a DATABASE_URL n�o estiver presente.
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<SeuDbContext>();
        // Use o m�todo `Database.Migrate()` para aplicar as migrations.
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Adiciona um log em caso de erro na migra��o para facilitar a depura��o.
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations.");
    }
}

// Habilita o Swagger e a sua UI em todos os ambientes
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    // Outras configura��es de desenvolvimento podem ficar aqui.
}

//app.UseHttpsRedirection();

// Usa a pol�tica de CORS definida
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();