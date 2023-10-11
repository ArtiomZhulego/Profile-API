FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Profile-API.csproj", "."]
RUN dotnet restore "./Profile-API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Profile-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile-API.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile-API.dll"]