FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

RUN sed -i 's/\[openssl_init\]/# [openssl_init]/' /etc/ssl/openssl.cnf

RUN printf "\n\n[openssl_init]\nssl_conf = ssl_sect" >> /etc/ssl/openssl.cnf
RUN printf "\n\n[ssl_sect]\nsystem_default = ssl_default_sect" >> /etc/ssl/openssl.cnf
RUN printf "\n\n[ssl_default_sect]\nMinProtocol = TLSv1\nCipherString = DEFAULT@SECLEVEL=0\n" >> /etc/ssl/openssl.cnf

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