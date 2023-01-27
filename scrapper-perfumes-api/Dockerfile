#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["scrapper-perfumes-api/scrapper-perfumes-api.csproj", "scrapper-perfumes-api/"]
COPY ["scrapper-perfumes-yves-common/scrapper-perfumes-yves-common.csproj", "scrapper-perfumes-yves-common/"]
RUN dotnet restore "scrapper-perfumes-api/scrapper-perfumes-api.csproj"
COPY . .
WORKDIR "/src/scrapper-perfumes-api"
RUN dotnet build "scrapper-perfumes-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "scrapper-perfumes-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "scrapper-perfumes-api.dll"]