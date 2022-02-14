# FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
# WORKDIR /0effort-crm-api
# COPY . .
# RUN dotnet restore
# RUN dotnet publish -o /0effort-crm-api/app

# FROM mcr.microsoft.com/dotnet/sdk:6.0 as runtime
# WORKDIR /0effort-crm-api
# COPY --from=build /0effort-crm-api/ /app
# ENTRYPOINT [ "dotnet", "/0effort-crm-api.dll" ]


#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /0-effort-crm
COPY ["/0-effort-crm/0-effort-crm", "0-effort-crm/"]
RUN dotnet restore "0-effort-crm/0-effort-crm.csproj"
COPY . .
WORKDIR "/src/0-effort-crm"
RUN dotnet build "0-effort-crm.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "0-effort-crm.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "docker-test.dll"]