FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore "./WebUI/WebCrawler.UI/WebCrawler.UI.csproj"
RUN dotnet publish "./WebUI/WebCrawler.UI/WebCrawler.UI.csproj" -c release -o /app --no-restore
RUN dotnet restore "./Crawler/Utils/Utils.csproj"
RUN dotnet publish "./Crawler/Utils/Utils.csproj" -c release -o /app --no-restore
RUN dotnet restore "./Crawler/WebCrawler.Application/WebCrawler.Application.csproj"
RUN dotnet publish "./Crawler/WebCrawler.Application/WebCrawler.Application.csproj" -c release -o /app --no-restore
RUN dotnet restore "./Crawler/WebCrawler.Core/WebCrawler.Core.csproj"
RUN dotnet publish "./Crawler/WebCrawler.Core/WebCrawler.Core.csproj" -c release -o /app --no-restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 7000

ENTRYPOINT ["dotnet", "WebCrawler.UI.dll"]