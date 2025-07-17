# Use ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy only the project file first
COPY QRScanner.csproj ./

# Restore dependencies
RUN dotnet restore QRScanner.csproj

# Copy the rest of the source code
COPY . ./

# Publish the application
RUN dotnet publish QRScanner.csproj -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "QRScanner.dll"]
