# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Adjust paths to include TaskTraker directory
COPY ["TaskTraker/TaskTraker.Api/TaskTraker.Api.csproj", "TaskTraker.Api/"]
COPY ["TaskTraker/TaskTraker.Data/TaskTraker.Data.csproj", "TaskTraker.Data/"]
COPY ["TaskTraker/TaskTraker.Services/TaskTraker.Services.csproj", "TaskTraker.Services/"]

# Restore dependencies
RUN dotnet restore "TaskTraker.Api/TaskTraker.Api.csproj"

# Copy all source code
COPY TaskTraker .

# Build the application
WORKDIR "/src/TaskTraker.Api"
RUN dotnet build "TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTraker.Api.dll"]
