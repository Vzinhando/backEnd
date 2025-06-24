# --- Estágio de Build ---
# Usa a imagem do SDK do .NET 8 para construir o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de solução (.sln) e de projeto (.csproj) primeiro.
# Isso é uma otimização de cache. Assumimos que eles estão na raiz.
COPY *.sln .
COPY *.csproj .
RUN dotnet restore

# Copia o resto dos arquivos do código-fonte.
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# --- Estágio Final ---
# Usa a imagem menor do ASP.NET Runtime, que é suficiente para rodar a aplicação.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados do estágio de build.
COPY --from=build /app/publish .

# Expõe a porta que a aplicação vai rodar. O Railway vai mapear isso automaticamente.
EXPOSE 8080

# Define o comando para iniciar a aplicação.
# Substitua 'BackEndDemoday.dll' pelo nome do seu arquivo .dll principal se for diferente.
ENTRYPOINT ["dotnet", "BackEndDemoday.dll"]
