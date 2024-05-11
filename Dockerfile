#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR "/"
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AccountMgt.Api/AccountMgt.Api.csproj", "AccountMgt.Api/"]
COPY ["AccountMgt.Auth/AccountMgt.Auth.csproj", "AccountMgt.Auth/"]
COPY ["AccountMgt.Model/AccountMgt.Model.csproj", "AccountMgt.Model/"]
COPY ["AccountMgt.Core/AccountMgt.Core.csproj", "AccountMgt.Core/"]
COPY ["AccountMgt.Data/AccountMgt.Data.csproj", "AccountMgt.Data/"]
COPY ["AccountMgt.Utility/AccountMgt.Utility.csproj", "AccountMgt.Utility/"]
RUN dotnet restore "./AccountMgt.Api/./AccountMgt.Api.csproj"
COPY . .
WORKDIR "/"
RUN dotnet build "./AccountMgt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AccountMgt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR "/"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountMgt.Api.dll"]