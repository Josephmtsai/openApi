#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["provider/provider.csproj", "provider/"]
RUN dotnet restore "provider/provider.csproj"
COPY . .
WORKDIR "/src/provider"
RUN dotnet build "provider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "provider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet NetCoreReactHeroku.dll