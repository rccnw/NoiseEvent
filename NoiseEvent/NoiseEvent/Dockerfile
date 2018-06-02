FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 53020
EXPOSE 44310

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY NoiseEvent/NoiseEvent.csproj NoiseEvent/
RUN dotnet restore NoiseEvent/NoiseEvent.csproj
COPY . .
WORKDIR /src/NoiseEvent
RUN dotnet build NoiseEvent.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NoiseEvent.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NoiseEvent.dll"]
