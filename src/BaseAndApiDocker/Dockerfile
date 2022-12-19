#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BaseAndApiDocker/BaseAndApiDocker.csproj", "BaseAndApiDocker/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Models/Models.csproj", "Models/"]`
RUN dotnet restore "BaseAndApiDocker/BaseAndApiDocker.csproj"
COPY . .
WORKDIR "/src/BaseAndApiDocker"
RUN dotnet build "BaseAndApiDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseAndApiDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseAndApiDocker.dll"]
