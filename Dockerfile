FROM mcr.microsoft.com/dotnet/sdk:8.0.121 AS build-env

# define /app como diretório padrão dentro do container
WORKDIR /app

# copia tudo para o /app
COPY src/ . 

# entra no projeto da api
WORKDIR /app/Ativos.Api

# baixa todas as dependências NuGet
RUN dotnet restore

# compila a aplicação em modo Release
RUN dotnet publish -c release -o /app/out

# executar api
FROM mcr.microsoft.com/dotnet/aspnet:8.0.121

# define /app como diretório principal
WORKDIR /app

# copia somente o resultado do publish
COPY --from=build-env /app/out .

# executa a api
ENTRYPOINT [ "dotnet", "Ativos.Api.dll"]