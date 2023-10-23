FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Profile-API/Profile-API.csproj", "Profile-API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Services.Abstraction/Services.Abstraction.csproj", "Services.Abstraction/"]

RUN dotnet restore "Profile-API\Profile-API.csproj"
COPY . .
WORKDIR "/src/Profile-API"
RUN dotnet build "Profile-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile-API.dll"]