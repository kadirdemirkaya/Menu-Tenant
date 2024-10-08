FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auth/Auth.Api/Auth.Api.csproj", "Auth/Auth.Api/"]
COPY ["Auth/Auth.Application/Auth.Application.csproj", "Auth/Auth.Application/"]
COPY ["Shared/Shared.Application/Shared.Application.csproj", "Shared/Shared.Application/"]
COPY ["Shared/Shared.Domain/Shared.Domain.csproj", "Shared/Shared.Domain/"]
COPY ["Auth/Auth.Infrastructure/Auth.Infrastructure.csproj", "Auth/Auth.Infrastructure/"]
COPY ["Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared/Shared.Infrastructure/"]
COPY ["Shared/Shared.Stream/Shared.Stream.csproj", "Shared/Shared.Stream/"]
RUN dotnet restore "./Auth/Auth.Api/Auth.Api.csproj" --source ./packages --source https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/Auth/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]