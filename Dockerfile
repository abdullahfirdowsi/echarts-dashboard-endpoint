# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything first to preserve folder structure
COPY . ./

# Debugging: Check if the Data directory exists in source
RUN find /app -name "*.xlsx" || echo "No Excel files found in source"

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

# Explicitly copy Data directory from source to runtime image
COPY --from=build /app/Data /app/Data

# Debugging: Verify Excel file was copied
RUN find /app -name "*.xlsx" || echo "No Excel files found in final image"

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

# Create a debug startup script
RUN echo '#!/bin/sh\necho "Checking for Excel files:"\nfind /app -name "*.xlsx"\necho "Starting application..."\nexec dotnet InternsApi.dll' > /app/start.sh \
    && chmod +x /app/start.sh

# Start the application
ENTRYPOINT ["/app/start.sh"]
