# Estágio 1: Build da Aplicação
# Usamos a imagem do SDK do .NET 8 (ou a versão que você usa) que contém todas as ferramentas de compilação.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Otimização: Copia primeiro os arquivos de projeto (.csproj e .sln) e restaura os pacotes NuGet.
# Isso cria um "cache". Se você só mudar o código C# depois, o Docker não precisará baixar os pacotes de novo.
COPY *.sln .
COPY BackendDemoday/*.csproj ./BackendDemoday/
RUN dotnet restore

# Agora copia todo o resto do código fonte da sua aplicação.
COPY . .

# Publica a aplicação em modo Release, criando uma versão otimizada na pasta "out".
WORKDIR /app/BackendDemoday
RUN dotnet publish -c Release -o /app/out


# Estágio 2: Imagem Final
# Usamos a imagem de runtime do ASP.NET, que é muito menor e mais segura que a do SDK.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia apenas o resultado da publicação do estágio de build para a imagem final.
COPY --from=build /app/out .

# Define o comando que será executado quando o contêiner iniciar.
# ATENÇÃO: Verifique se o nome do seu .dll está correto!
ENTRYPOINT ["dotnet", "BackendDemoday.dll"]