FROM mcr.microsoft.com/dotnet/core/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./WebUI/
RUN dotnet restore

COPY *.csproj ./Crawler/Utils/
RUN dotnet restore

COPY *.csproj ./Crawler/WebCrawler.Application/
RUN dotnet restore

COPY *.csproj ./Crawler/WebCrawler.Core/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebCrawler.dll"]