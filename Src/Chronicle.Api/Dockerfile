#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Chronicle.Api/Chronicle.Api.csproj", "Src/Chronicle.Api/"]
COPY ["Src/Chronicle.Application/Chronicle.Application.csproj", "Src/Chronicle.Application/"]
COPY ["Src/Chronicle.Domain/Chronicle.Domain.csproj", "Src/Chronicle.Domain/"]
COPY ["Src/Chronicle.Infrastucture/Chronicle.Infrastucture.csproj", "Src/Chronicle.Infrastucture/"]
COPY ["Src/Chronicle.PostgrePersistence/Chronicle.PostgrePersistence.csproj", "Src/Chronicle.PostgrePersistence/"]
RUN dotnet restore "./Src/Chronicle.Api/Chronicle.Api.csproj"
COPY . .
WORKDIR "/src/Src/Chronicle.Api"
RUN dotnet build "./Chronicle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Chronicle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chronicle.Api.dll"]