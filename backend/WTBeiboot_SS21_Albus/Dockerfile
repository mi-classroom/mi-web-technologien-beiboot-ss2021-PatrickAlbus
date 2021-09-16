#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WTBeiboot_SS21_Albus/WTBeiboot_SS21_Albus.csproj", "WTBeiboot_SS21_Albus/"]
RUN dotnet restore "WTBeiboot_SS21_Albus/WTBeiboot_SS21_Albus.csproj"
COPY . .
WORKDIR "/src/WTBeiboot_SS21_Albus"
RUN dotnet build "WTBeiboot_SS21_Albus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WTBeiboot_SS21_Albus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WTBeiboot_SS21_Albus.dll"]