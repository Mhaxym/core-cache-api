


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App

COPY --from=build-env /App/out .

COPY ["servercert.pfx", "/https/servercert.pfx"]
COPY ["cacert.crt", "/usr/local/share/ca-certificates/cacert.crt"]
RUN update-ca-certificates

ENTRYPOINT ["dotnet", "CoreCacheAPI.dll"]

