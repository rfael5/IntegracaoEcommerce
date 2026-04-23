FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5227
ENV ASPNETCORE_URLS=http://+:5227

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["IntegracaoEcommerce.csproj", "./"]
RUN dotnet restore "IntegracaoEcommerce.csproj"
COPY . .
WORKDIR "/src/."

RUN dotnet build "IntegracaoEcommerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegracaoEcommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntegracaoEcommerce.dll"]