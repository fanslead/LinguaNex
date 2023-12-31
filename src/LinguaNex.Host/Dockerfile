#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LinguaNex.Host/LinguaNex.Host.csproj", "src/LinguaNex.Host/"]
COPY ["src/LinguaNex.Api/LinguaNex.Api.csproj", "src/LinguaNex.Api/"]
COPY ["src/LinguaNex.Application/LinguaNex.Application.csproj", "src/LinguaNex.Application/"]
COPY ["src/LinguaNex.Data/LinguaNex.Data.csproj", "src/LinguaNex.Data/"]
COPY ["src/LinguaNex.Domain/LinguaNex.Domain.csproj", "src/LinguaNex.Domain/"]
COPY ["src/LinguaNex.Shared/LinguaNex.Shared.csproj", "src/LinguaNex.Shared/"]
RUN dotnet restore "./src/LinguaNex.Host/./LinguaNex.Host.csproj"
COPY . .
WORKDIR "/src/src/LinguaNex.Host"
RUN dotnet build "./LinguaNex.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LinguaNex.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinguaNex.Host.dll"]