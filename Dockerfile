FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/BurgerRoyale.Payment.API/BurgerRoyale.Payment.API.csproj", "src/BurgerRoyale.Payment.API/"]
COPY ["src/BurgerRoyale.Payment.Application/BurgerRoyale.Payment.Application.csproj", "src/BurgerRoyale.Payment.Application/"]
COPY ["src/BurgerRoyale.Payment.Domain/BurgerRoyale.Payment.Domain.csproj", "src/BurgerRoyale.Payment.Domain/"]
COPY ["src/BurgerRoyale.Payment.BackgroundService/BurgerRoyale.Payment.BackgroundService.csproj", "src/BurgerRoyale.Payment.BackgroundService/"]
COPY ["src/BurgerRoyale.Payment.IOC/BurgerRoyale.Payment.IOC.csproj", "src/BurgerRoyale.Payment.IOC/"]
COPY ["src/BurgerRoyale.Payment.Infrastructure/BurgerRoyale.Payment.Infrastructure.csproj", "src/BurgerRoyale.Payment.Infrastructure/"]
RUN dotnet restore "src/BurgerRoyale.Payment.API/BurgerRoyale.Payment.API.csproj"
COPY . .
WORKDIR "/src/src/BurgerRoyale.Payment.API"
RUN dotnet build "BurgerRoyale.Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BurgerRoyale.Payment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BurgerRoyale.Payment.API.dll"]