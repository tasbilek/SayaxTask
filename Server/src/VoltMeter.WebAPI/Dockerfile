FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Server/src/VoltMeter.WebAPI/VoltMeter.WebAPI.csproj", "Server/src/VoltMeter.WebAPI/"]
RUN dotnet restore "Server/src/VoltMeter.WebAPI/VoltMeter.WebAPI.csproj"
COPY . .
WORKDIR "/src/Server/src/VoltMeter.WebAPI"
RUN dotnet build "VoltMeter.WebAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "VoltMeter.WebAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoltMeter.WebAPI.dll"]
