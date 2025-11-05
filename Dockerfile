# Etapa 1 — Build do projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o restante do código e faz o publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 — Runtime (executar a aplicação)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que o Railway vai expor
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Inicia o projeto
ENTRYPOINT ["dotnet", "Back_For_Portifolio.dll"]
