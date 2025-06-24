# Usa a imagem do SDK do .NET 8 para construir o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto (.csproj) e a solução (.sln) primeiro.
# Isso otimiza o cache do Docker. Se os projetos não mudarem, o 'dotnet restore' não precisa rodar de novo.
COPY *.sln .
COPY BackEndDemoday/*.csproj ./BackEndDemoday/
RUN dotnet restore

# Copia todo o resto do código fonte do projeto.
COPY BackEndDemoday/. ./BackEndDemoday/

# Publica a aplicação, criando os arquivos otimizados para release.
WORKDIR /app/BackEndDemoday
RUN dotnet publish -c release -o /app/publish --no-restore

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
