FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR /App

COPY . ./
WORKDIR /App/orgcat.web
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /App
COPY --from=build-env /App/orgcat.web/out .
ENTRYPOINT ["dotnet", "orgcat.web.dll"]
