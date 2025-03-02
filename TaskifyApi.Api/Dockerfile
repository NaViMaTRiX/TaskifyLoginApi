FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskifyApi.Api/TaskifyApi.Api.csproj", "TaskifyApi.Api/"]
COPY ["TaskifyApi.Domain/TaskifyApi.Domain.csproj", "TaskifyApi.Domain/"]
COPY ["TaskifyApi.Application/TaskifyApi.Application.csproj", "TaskifyApi.Application/"]
COPY ["TaskifyApi.DAL/TaskifyApi.DAL.csproj", "TaskifyApi.DAL/"]
RUN dotnet restore "TaskifyApi.Api/TaskifyApi.Api.csproj"
COPY . .
WORKDIR "/src/TaskifyApi.Api"
RUN dotnet build "TaskifyApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskifyApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskifyApi.Api.dll"]
