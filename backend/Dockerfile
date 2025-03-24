# Use the official .NET 8 SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copy & build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyProject.csproj", "./"]
RUN dotnet restore "./MyProject.csproj"

COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Run migrations before starting the app
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet MyProject.dll"]
