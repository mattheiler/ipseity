#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WispyWaterfall.Web/WispyWaterfall.Web.csproj", "WispyWaterfall.Web/"]
COPY ["WispyWaterfall.Infrastructure/WispyWaterfall.Infrastructure.csproj", "WispyWaterfall.Infrastructure/"]
COPY ["WispyWaterfall.Core/WispyWaterfall.Core.csproj", "WispyWaterfall.Core/"]
RUN dotnet restore "WispyWaterfall.Web/WispyWaterfall.Web.csproj"
COPY . .
WORKDIR "/src/WispyWaterfall.Web"
RUN dotnet build "WispyWaterfall.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WispyWaterfall.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WispyWaterfall.Web.dll"]