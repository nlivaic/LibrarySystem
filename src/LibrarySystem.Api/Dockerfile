#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/LibrarySystem.Api/LibrarySystem.Api.csproj", "src/LibrarySystem.Api/"]
COPY ["src/LibrarySystem.Application/LibrarySystem.Application.csproj", "src/LibrarySystem.Application/"]
COPY ["src/LibrarySystem.Core/LibrarySystem.Core.csproj", "src/LibrarySystem.Core/"]
COPY ["src/LibrarySystem.Common/LibrarySystem.Common.csproj", "src/LibrarySystem.Common/"]
COPY ["src/LibrarySystem.Data/LibrarySystem.Data.csproj", "src/LibrarySystem.Data/"]
RUN dotnet restore "src/LibrarySystem.Api/LibrarySystem.Api.csproj"
COPY . .
WORKDIR "/src/src/LibrarySystem.Api"
RUN dotnet build "LibrarySystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibrarySystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrarySystem.Api.dll"]
