# Estágio 1: Build da Aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.sln .
COPY backEnd/*.csproj ./backEnd/  # <-- MUDANÇA 1
RUN dotnet restore

# Copia todo o resto do código fonte da sua aplicação.
COPY . .

# Publica a aplicação em modo Release
WORKDIR /app/backEnd # <-- MUDANÇA 2
RUN dotnet publish -c Release -o /app/out


# Estágio 2: Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia a aplicação publicada do estágio de build
COPY --from=build /app/out .

# Define o comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "backEnd.dll"] # <-- MUDANÇA 3