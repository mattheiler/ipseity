#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Example.Web/Example.Web.csproj", "Example.Web/"]
COPY ["Example.Infrastructure/Example.Infrastructure.csproj", "Example.Infrastructure/"]
COPY ["Example.Core/Example.Core.csproj", "Example.Core/"]
RUN dotnet restore "Example.Web/Example.Web.csproj"
COPY . .
WORKDIR "/src/Example.Web"
RUN dotnet build "Example.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Web.dll"]