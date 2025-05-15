# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything first to preserve folder structure
COPY . ./

# Restore dependencies
RUN dotnet restore "InternsApi.csproj"

# Build and publish the application
RUN dotnet build "InternsApi.csproj" -c Release -o /app/build
RUN dotnet publish "InternsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Copy the published application
COPY --from=build /app/publish .

# Create a non-root user for security
RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* \
    && adduser --disabled-password --gecos "" appuser \
    && chown -R appuser /app
USER appuser

# Expose the port the API will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "InternsApi.dll"]
