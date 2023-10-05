# Use the .NET Core 3.1 SDK as the base image for the build stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy the csproj file(s) and restore any dependencies (via nuget)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application to /app/out directory in the container
RUN dotnet publish -c Release -o out

# Use the .NET Core 3.1 runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 80

# Define the command to run the application
ENTRYPOINT ["dotnet", "DemoAPI.dll"]
