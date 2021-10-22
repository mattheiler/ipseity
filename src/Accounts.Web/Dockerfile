#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Accounts.Core/Accounts.Core.csproj", "Accounts.Core/"]
COPY ["Accounts.Infrastructure/Accounts.Infrastructure.csproj", "Accounts.Infrastructure/"]
COPY ["Accounts.Web/Accounts.Web.csproj", "Accounts.Web/"]
RUN dotnet restore "Accounts.Web/Accounts.Web.csproj"
COPY . .
WORKDIR "/src/Accounts.Web"
RUN dotnet build "Accounts.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Web.dll"]
