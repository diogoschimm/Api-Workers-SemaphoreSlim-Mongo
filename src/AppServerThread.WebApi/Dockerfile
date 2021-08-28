#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/AppServerThread.WebApi/AppServerThread.WebApi.csproj", "src/AppServerThread.WebApi/"]
COPY ["src/AppServerThread.Core/AppServerThread.Core.csproj", "src/AppServerThread.Core/"]
RUN dotnet restore "src/AppServerThread.WebApi/AppServerThread.WebApi.csproj"
COPY . .
WORKDIR "/src/src/AppServerThread.WebApi"
RUN dotnet build "AppServerThread.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppServerThread.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppServerThread.WebApi.dll"]