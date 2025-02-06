FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files first
COPY ["Web/Web.csproj", "Web/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Repository/Repository.csproj", "Repository/"]

# Restore all packages
RUN dotnet restore "Web/Web.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Service/Service.csproj"
RUN dotnet restore "Repository/Repository.csproj"

# Copy the rest of the source code
COPY . .

# Build the solution
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]