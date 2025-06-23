# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.sln .
COPY BackEndDemoday/*.csproj ./BackEndDemoday/
RUN dotnet restore

# Copia todo o resto do código e publica a aplicação
COPY . .
WORKDIR /app/BackEndDemoday
RUN dotnet publish -c Release -o /app/out


# Estágio 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia a aplicação publicada do estágio de build
COPY --from=build /app/out .

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "BackEndDemoday.dll"]