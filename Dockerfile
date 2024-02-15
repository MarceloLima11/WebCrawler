FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY WebUI/*.csproj ./WebUI/
RUN dotnet restore "./WebUI/WebCrawler.UI/WebCrawler.UI.csproj"
COPY Crawler/Utils/*.csproj ./Crawler/Utils/
RUN dotnet restore "./Crawler/Utils/Utils.csproj"
COPY Crawler/WebCrawler.Application/*.csproj ./Crawler/WebCrawler.Application/
RUN dotnet restore "./Crawler/WebCrawler.Application/WebCrawler.Application.csproj"
COPY Crawler/WebCrawler.Core/*.csproj ./Crawler/WebCrawler.Core/
RUN dotnet restore "./Crawler/WebCrawler.Core/WebCrawler.Core.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WebCrawler.dll"]