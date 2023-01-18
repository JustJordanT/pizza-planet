FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PizzaPlanet.Kitchen.API/PizzaPlanet.Kitchen.API.csproj", "PizzaPlanet.Kitchen.API/"]
RUN dotnet restore "PizzaPlanet.Kitchen.API/PizzaPlanet.Kitchen.API.csproj"
COPY . .
WORKDIR "/src/PizzaPlanet.Kitchen.API"
RUN dotnet build "PizzaPlanet.Kitchen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaPlanet.Kitchen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaPlanet.Kitchen.API.dll"]
