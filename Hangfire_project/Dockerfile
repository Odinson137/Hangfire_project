FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Hangfire_project/Hangfire_project.csproj", "Hangfire_project/"]
RUN dotnet restore "Hangfire_project/Hangfire_project.csproj"
COPY . .
WORKDIR "/src/Hangfire_project"
RUN dotnet build "Hangfire_project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hangfire_project.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hangfire_project.dll"]
