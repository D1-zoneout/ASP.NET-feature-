# Step 1: Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . . 
RUN dotnet restore

# Build and publish the app to /app/publish
RUN dotnet publish -c Release -o /app/publish

# Step 2: Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Run tWebApplication1
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
