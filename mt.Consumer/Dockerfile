#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mt.Consumer/mt.Consumer.csproj", "mt.Consumer/"]
RUN dotnet restore "mt.Consumer/mt.Consumer.csproj"
COPY . .
WORKDIR "/src/mt.Consumer"
RUN dotnet build "mt.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mt.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mt.Consumer.dll"]