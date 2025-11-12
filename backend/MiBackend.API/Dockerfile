# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución y proyectos .csproj desde la raíz relativa
COPY ../*.sln ./
COPY ../MiBackend.API/*.csproj MiBackend.API/
COPY ../MiBackend.Data/*.csproj MiBackend.Data/
COPY ../MiBackend.Domain/*.csproj MiBackend.Domain/

# Restaurar paquetes
RUN dotnet restore

# Copiar todo el código fuente
COPY ../ ./

# Publicar el proyecto API
WORKDIR /src/MiBackend.API
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los binarios publicados desde build
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MiBackend.API.dll"]
