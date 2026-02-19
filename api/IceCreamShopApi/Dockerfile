FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["IceCreamShopApi.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release \ 
    -o /app/publish \
    -r linux-x64 \
    --self-contained true \
    -p:PublishTrimmed=true \
    -p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:10.0-noble-chiseled-extra AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["./IceCreamShopApi"]