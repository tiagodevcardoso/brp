#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BRP.Services.Publish.MassTransit.Generic/BRP.Services.Publish.MassTransit.Generic.csproj", "BRP.Services.Publish.MassTransit.Generic/"]
RUN dotnet restore "BRP.Services.Publish.MassTransit.Generic/BRP.Services.Publish.MassTransit.Generic.csproj"
COPY . .
WORKDIR "/src/BRP.Services.Publish.MassTransit.Generic"
RUN dotnet build "BRP.Services.Publish.MassTransit.Generic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BRP.Services.Publish.MassTransit.Generic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BRP.Services.Publish.MassTransit.Generic.dll"]