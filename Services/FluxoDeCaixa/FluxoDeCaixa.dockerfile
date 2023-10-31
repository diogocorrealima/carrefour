### Estágio 1 - Obter o source e gerar o Build ###
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dotnet-builder
WORKDIR /app

COPY . .

ENV ASPNETCORE_ENVIRONMENT Staging

 RUN dotnet test ./src/FluxoDeCaixa.UnitTests --logger "trx;LogFileName=/app/publish/testresultsUnit.trx"

RUN dotnet publish ./src/FluxoDeCaixa.Services.Api/FluxoDeCaixa.Services.Api.csproj -o /app/publish 

### Estágio 2 - Subir a aplicação através dos binários ###
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
COPY --from=dotnet-builder /app/publish .
RUN ls -al
ENTRYPOINT ["dotnet", "FluxoDeCaixa.Services.Api.dll"]