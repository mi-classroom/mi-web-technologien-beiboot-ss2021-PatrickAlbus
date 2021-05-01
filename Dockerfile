#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WTBeiboot_SS21_Albus.Logger/WTBeiboot_SS21_Albus.Logger.csproj", "WTBeiboot_SS21_Albus.Logger/"]
COPY ["WTBeiboot_SS21_Albus/WTBeiboot_SS21_Albus.csproj", "WTBeiboot_SS21_Albus/"]
COPY ["WTBeiboot_SS21_Albus.Service.Contracts/WTBeiboot_SS21_Albus.Service.Contracts.csproj", "WTBeiboot_SS21_Albus.Service.Contracts/"]
COPY ["WTBeiboot_SS21_Albus.Service/WTBeiboot_SS21_Albus.Service.csproj", "WTBeiboot_SS21_Albus.Service/"]
RUN dotnet restore "WTBeiboot_SS21_Albus/WTBeiboot_SS21_Albus.csproj"
COPY . .
WORKDIR "/src/WTBeiboot_SS21_Albus"
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
RUN npm install
RUN dotnet build "WTBeiboot_SS21_Albus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WTBeiboot_SS21_Albus.csproj" -c Release -o /app/publish

FROM base AS final
# Add a /app volume
#VOLUME ["WTBeiboot_SS21_Albus/wwwroot/data"]
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WTBeiboot_SS21_Albus.dll"]