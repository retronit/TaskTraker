# Build stage using Windows Server Core
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["TaskTraker/TaskTraker.Api/TaskTraker.Api.csproj", "TaskTraker.Api/"]
COPY ["TaskTraker/TaskTraker.Data/TaskTraker.Data.csproj", "TaskTraker.Data/"]
COPY ["TaskTraker/TaskTraker.Services/TaskTraker.Services.csproj", "TaskTraker.Services/"]

RUN dotnet restore "TaskTraker.Api/TaskTraker.Api.csproj"

COPY TaskTraker .

WORKDIR "/src/TaskTraker.Api"
RUN dotnet build "TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "TaskTraker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Switch to Windows Server Core base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTraker.Api.dll"]
