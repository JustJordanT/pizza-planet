FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["pizza-planet/PizzaPlanet.API/PizzaPlanet.API.csproj", "PizzaPlanet.API/"]
RUN dotnet restore "PizzaPlanet.API/PizzaPlanet.API.csproj"
COPY . .
WORKDIR "/src/PizzaPlanet.API"
RUN dotnet build "PizzaPlanet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaPlanet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaPlanet.API.dll"]
