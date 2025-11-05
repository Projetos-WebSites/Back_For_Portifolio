# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo (incluindo o csproj e subpastas)
COPY . .

# Restaura dependências e publica o projeto principal
RUN dotnet restore "Back_For_Portifolio.csproj"
RUN dotnet publish "Back_For_Portifolio.csproj" -c Release -o /app/publish

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Define a URL e a porta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Inicia o aplicativo
ENTRYPOINT ["dotnet", "Back_For_Portifolio.dll"]
