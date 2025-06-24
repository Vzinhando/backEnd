FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução (.sln) da raiz.
COPY *.sln .

# Copia o arquivo de projeto (.csproj) que está dentro da subpasta.
COPY BackEndDemoday/*.csproj ./BackEndDemoday/

# Restaura as dependências. É importante especificar o caminho do projeto.
RUN dotnet restore "./BackEndDemoday/BackEndDemoday.csproj"

# Copia todo o resto do código-fonte para a sua respectiva pasta.
COPY BackEndDemoday/. ./BackEndDemoday/

# Define o diretório de trabalho para a pasta do projeto antes de publicar.
WORKDIR "/src/BackEndDemoday"
RUN dotnet publish "BackEndDemoday.csproj" -c Release -o /app/publish

# --- Estágio Final ---
# Usa a imagem menor do ASP.NET Runtime, que é suficiente para rodar a aplicação.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados do estágio de build.
COPY --from=build /app/publish .

# Define as variáveis de ambiente para garantir que a aplicação rode em produção e na porta correta.
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Expõe a porta que a aplicação vai rodar. O Railway vai mapear isso automaticamente.
EXPOSE 8080

# Define o comando para iniciar a aplicação.
# O nome do .dll deve estar correto.
ENTRYPOINT ["dotnet", "BackEndDemoday.dll"]
