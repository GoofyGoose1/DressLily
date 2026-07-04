FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Store.sln ./
COPY LilyPixelCatShop/Store.csproj LilyPixelCatShop/
RUN dotnet restore LilyPixelCatShop/Store.csproj

COPY LilyPixelCatShop/ LilyPixelCatShop/
RUN dotnet publish LilyPixelCatShop/Store.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} dotnet Store.dll"]
