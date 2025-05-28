FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o .csproj primeiro para otimizar o cache
COPY ["src/LojaDoManoel.Api/LojaDoManoel.Api.csproj", "src/LojaDoManoel.Api/"]
RUN dotnet restore "src/LojaDoManoel.Api/LojaDoManoel.Api.csproj"

# Copia todo o resto
COPY . .

# Publica a aplicação
WORKDIR "/src/src/LojaDoManoel.Api"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "LojaDoManoel.Api.dll"]