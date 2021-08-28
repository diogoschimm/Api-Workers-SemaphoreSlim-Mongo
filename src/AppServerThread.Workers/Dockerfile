#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/AppServerThread.Workers/AppServerThread.Workers.csproj", "src/AppServerThread.Workers/"]
COPY ["src/AppServerThread.Core/AppServerThread.Core.csproj", "src/AppServerThread.Core/"]
RUN dotnet restore "src/AppServerThread.Workers/AppServerThread.Workers.csproj"
COPY . .
WORKDIR "/src/src/AppServerThread.Workers"
RUN dotnet build "AppServerThread.Workers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppServerThread.Workers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppServerThread.Workers.dll"]
