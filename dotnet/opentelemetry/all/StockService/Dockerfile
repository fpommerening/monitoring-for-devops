FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim
EXPOSE 6001/TCP
WORKDIR /app/
COPY out ./

ENTRYPOINT ["./StockService"]