FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Установим переменные окружения
ENV ASPNETCORE_URLS=http://+:8080;http://+:8081
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sulpak.TestAssigment.PublicApi/Sulpak.TestAssigment.PublicApi.csproj", "Sulpak.TestAssigment.PublicApi/"]
COPY ["Sulpak.TestAssigment.Application/Sulpak.TestAssigment.Application.csproj", "Sulpak.TestAssigment.Application/"]
COPY ["Sulpak.TestAssigment.Domain/Sulpak.TestAssigment.Domain.csproj", "Sulpak.TestAssigment.Domain/"]
COPY ["Sulpak.TestAssigment.Infrastructure/Sulpak.TestAssigment.Infrastructure.csproj", "Sulpak.TestAssigment.Infrastructure/"]
COPY ["Sulpak.TestAssigment.SharedKernel/Sulpak.TestAssigment.SharedKernel.csproj", "Sulpak.TestAssigment.SharedKernel/"]
RUN dotnet restore "Sulpak.TestAssigment.PublicApi/Sulpak.TestAssigment.PublicApi.csproj"
COPY . .
WORKDIR "/src/Sulpak.TestAssigment.PublicApi"
RUN dotnet build "Sulpak.TestAssigment.PublicApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sulpak.TestAssigment.PublicApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sulpak.TestAssigment.PublicApi.dll"]
