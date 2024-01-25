#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Vehicle2Go.Web/Vehicle2Go.Web.csproj", "Vehicle2Go.Web/"]
COPY ["Vehicle2Go.Common/Vehicle2Go.Common.csproj", "Vehicle2Go.Common/"]
COPY ["Vehicle2Go.Data.Models/Vehicle2Go.Data.Models.csproj", "Vehicle2Go.Data.Models/"]
COPY ["Vehicle2Go.Data/Vehicle2Go.Data.csproj", "Vehicle2Go.Data/"]
COPY ["Vehicle2Go.Services.Data.Models/Vehicle2Go.Services.Data.Models.csproj", "Vehicle2Go.Services.Data.Models/"]
COPY ["Vehicle2Go.Web.ViewModels/Vehicle2Go.Web.ViewModels.csproj", "Vehicle2Go.Web.ViewModels/"]
COPY ["Vehicle2Go.Web.Vehicle2Go.Services.Data/Vehicle2Go.Services.Data.csproj", "Vehicle2Go.Services.Data/"]
COPY ["Vehicle2Go.Web/Vehicle2Go.Web.Infrastructure/Vehicle2Go.Web.Infrastructure.csproj", "Vehicle2Go.Web.Infrastructure/"]
RUN dotnet restore "./Vehicle2Go.Web/./Vehicle2Go.Web.csproj"
COPY . .
WORKDIR "/src/Vehicle2Go.Web"
RUN dotnet build "./Vehicle2Go.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Vehicle2Go.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicle2Go.Web.dll"]