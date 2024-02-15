FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY WebUI/*.csproj ./WebUI/
COPY Crawler/Utils/*.csproj ./Crawler/Utils/
COPY Crawler/WebCrawler.Application/*.csproj ./Crawler/WebCrawler.Application/
COPY Crawler/WebCrawler.Core/*.csproj ./Crawler/WebCrawler.Core/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebCrawler.dll"]