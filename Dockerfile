FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# ✅ Install fonts and fontconfig
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    fonts-dejavu-core \
    fonts-dejavu \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ✅ Expose port correctly for Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "WebApplication1.dll"]
