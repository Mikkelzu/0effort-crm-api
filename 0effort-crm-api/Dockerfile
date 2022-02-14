#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["0effort-crm-api/0effort-crm-api.csproj", "0effort-crm-api/"]
RUN dotnet restore "0effort-crm-api/0effort-crm-api.csproj"
COPY . .
WORKDIR "/src/0effort-crm-api"
RUN dotnet build "0effort-crm-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "0effort-crm-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "0effort-crm-api.dll"]