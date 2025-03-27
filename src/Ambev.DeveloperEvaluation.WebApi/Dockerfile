#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef --version 8.0.10
ENV PATH="$PATH:/root/.dotnet/tools"

# Install netcat for database check
RUN apt-get update && apt-get install -y netcat-openbsd && rm -rf /var/lib/apt/lists/*

COPY ["src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj", "src/Ambev.DeveloperEvaluation.WebApi/"]
COPY ["src/Ambev.DeveloperEvaluation.Application/Ambev.DeveloperEvaluation.Application.csproj", "src/Ambev.DeveloperEvaluation.Application/"]
COPY ["src/Ambev.DeveloperEvaluation.Common/Ambev.DeveloperEvaluation.Common.csproj", "src/Ambev.DeveloperEvaluation.Common/"]
COPY ["src/Ambev.DeveloperEvaluation.Domain/Ambev.DeveloperEvaluation.Domain.csproj", "src/Ambev.DeveloperEvaluation.Domain/"]
COPY ["src/Ambev.DeveloperEvaluation.IoC/Ambev.DeveloperEvaluation.IoC.csproj", "src/Ambev.DeveloperEvaluation.IoC/"]
COPY ["src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj", "src/Ambev.DeveloperEvaluation.ORM/"]

RUN dotnet restore "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"
COPY . .

# Ensure appsettings.json exists in ORM project
RUN mkdir -p /src/src/Ambev.DeveloperEvaluation.ORM/
COPY ["src/Ambev.DeveloperEvaluation.ORM/appsettings.json", "/src/src/Ambev.DeveloperEvaluation.ORM/"]

WORKDIR "/src/src/Ambev.DeveloperEvaluation.WebApi"
RUN dotnet publish "./Ambev.DeveloperEvaluation.WebApi.csproj" -c Release -o /app/publish

WORKDIR /app
COPY ["src/Ambev.DeveloperEvaluation.WebApi/entrypoint.sh", "/app/entrypoint.sh"]
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]