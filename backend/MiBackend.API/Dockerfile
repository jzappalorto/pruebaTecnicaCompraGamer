FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../*.sln ./
COPY ../MiBackend.API/*.csproj MiBackend.API/
COPY ../MiBackend.Data/*.csproj MiBackend.Data/
COPY ../MiBackend.Domain/*.csproj MiBackend.Domain/

RUN dotnet restore

COPY ../ ./

WORKDIR /src/MiBackend.API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MiBackend.API.dll"]
