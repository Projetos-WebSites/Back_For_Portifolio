# Usa a imagem oficial do .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia o csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e publica
COPY . ./
RUN dotnet publish -c Release -o out

# Usa a imagem ASP.NET para rodar o app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define a porta padrão
EXPOSE 8080

# Comando para iniciar o app
ENTRYPOINT ["dotnet", "Back_For_Portifolio.dll"]