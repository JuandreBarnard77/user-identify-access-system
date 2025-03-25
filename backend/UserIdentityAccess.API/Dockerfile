# Use .NET 8 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY ./UserIdentityAccess.sln ./
COPY ./UserIdentityAccess.Domain/*.csproj ./UserIdentityAccess.Domain/
COPY ./UserIdentityAccess.Application/*.csproj ./UserIdentityAccess.Application/
COPY ./UserIdentityAccess.Infrastructure/*.csproj ./UserIdentityAccess.Infrastructure/
COPY ./UserIdentityAccess.API/*.csproj ./UserIdentityAccess.API/
COPY ./UserIdentityAccess.Tests/*.csproj ./UserIdentityAccess.Tests/

RUN dotnet restore

# Copy the entire project and build
COPY . ./
WORKDIR /app/UserIdentityAccess.API
RUN mkdir -p /out
RUN dotnet publish -c Release -o /out

# Use the .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
RUN mkdir -p /app
COPY --from=build /out .

# Run EF Core migrations before starting the API
ENTRYPOINT ["sh", "-c", "dotnet ef database update --project ../UserIdentityAccess.Infrastructure/ && dotnet UserIdentityAccess.API.dll"]
