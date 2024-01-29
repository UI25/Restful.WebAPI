#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Use the official .NET Core Sdk as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#buld runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API/WebAPI.csproj", "src/API/"]
COPY ["src/Models/WebAPIModels.csproj", "src/Models/"]
COPY ["src/Repository/WebAPIRepository.csproj", "src/Repository/"]
RUN dotnet restore "src/API/WebAPI.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]