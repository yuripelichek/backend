#!/bin/bash

echo "Waiting for database to be ready..."
while ! nc -z ambev.developerevaluation.database 5432; do
  sleep 0.1
done
echo "Database is ready!"

echo "Running database migrations..."
cd /src/src/Ambev.DeveloperEvaluation.WebApi
dotnet ef database update \
    --project ../Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj \
    --startup-project Ambev.DeveloperEvaluation.WebApi.csproj \
    --connection "Host=ambev.developerevaluation.database;Database=DeveloperEvaluation;Username=postgres;Password=postgres;Port=5432"

echo "Starting the application..."
cd /app/publish
exec dotnet Ambev.DeveloperEvaluation.WebApi.dll
