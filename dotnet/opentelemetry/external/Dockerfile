FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
EXPOSE 80/TCP
WORKDIR /app/
COPY out ./

ENTRYPOINT ["./ExternalService"]