# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Authentication.Api/Authentication.Api.csproj", "Authentication.Api/"]
COPY ["Authentication.Application/Authentication.Application.csproj", "Authentication.Application/"]
COPY ["Authentication.Domain/Authentication.Domain.csproj", "Authentication.Domain/"]
COPY ["Authentication.Infrastructure/Authentication.Infrastructure.csproj", "Authentication.Infrastructure/"]
COPY ["Ecommerce.SharedService/SharedService.Lib/SharedService.Lib.csproj", "Ecommerce.SharedService/SharedService.Lib/"]
RUN dotnet restore "Authentication.Api/Authentication.Api.csproj"
COPY . .
WORKDIR "/src/Authentication.Api"
RUN dotnet build "./Authentication.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authentication.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]