# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e publica
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define a porta que o Railway usa
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Inicia o aplicativo
ENTRYPOINT ["dotnet", "Back_For_Portifolio.dll"]
