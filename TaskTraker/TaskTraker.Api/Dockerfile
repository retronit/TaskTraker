# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskTraker.Api/TaskTraker.Api.csproj", "TaskTraker.Api/"]
COPY ["TaskTraker.Data/TaskTraker.Data.csproj", "TaskTraker.Data/"]
COPY ["TaskTraker.Services/TaskTraker.Services.csproj", "TaskTraker.Services/"]
RUN dotnet restore "./TaskTraker.Api/TaskTraker.Api.csproj"
COPY . .
WORKDIR "/src/TaskTraker.Api"
RUN dotnet build "./TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "TaskTraker.Api.dll"]
