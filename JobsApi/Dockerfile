FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobsApi/JobsApi.csproj", "JobsApi/"]

ARG GITHUB_TOKEN
ARG GITHUB_NAME

RUN dotnet nuget add source --store-password-in-clear-text "https://nuget.pkg.github.com/Takasaki-Studio/index.json" --name github --username ${GITHUB_NAME} --password ${GITHUB_TOKEN}

RUN dotnet restore "JobsApi/JobsApi.csproj"
COPY . .
WORKDIR "/src/JobsApi"
RUN dotnet build "JobsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "JobsApi.dll"]