FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
EXPOSE 6001/TCP
WORKDIR /app/
COPY out ./

ENTRYPOINT ["./StockService"]