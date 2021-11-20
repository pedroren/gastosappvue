FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln ./
COPY GastosAppApi/GastosAppApi.csproj GastosAppApi/
COPY GastosAppCoreEF/GastosAppCoreEF.csproj GastosAppCoreEF/
#COPY GastosAppMvc/GastosAppMvc.csproj GastosAppMvc/
#COPY GastosAppCore/GastosAppCore.csproj GastosAppCore/
#COPY GastosAppDatabase/GastosAppDatabase.sqlproj GastosAppDatabase/
#COPY docker-compose.dcproj ./
WORKDIR /src/GastosAppCoreEF
RUN dotnet restore
WORKDIR /src/GastosAppApi
RUN dotnet restore
WORKDIR /src

COPY . .
WORKDIR /src/GastosAppApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GastosAppApi.dll"]
