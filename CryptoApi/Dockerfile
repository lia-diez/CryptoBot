FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.sln .
COPY *.csproj ./CryptoApi/
RUN dotnet restore ./CryptoApi/

COPY . ./CryptoApi/
WORKDIR /source/CryptoApi
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CryptoApi.dll"]
