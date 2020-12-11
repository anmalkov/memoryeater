FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MemoryEater/MemoryEater.csproj", "MemoryEater/"]
RUN dotnet restore "MemoryEater/MemoryEater.csproj"
COPY . .
WORKDIR "/src/MemoryEater"
RUN dotnet build "MemoryEater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MemoryEater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MemoryEater.dll"]