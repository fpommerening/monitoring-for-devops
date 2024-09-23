FROM mcr.microsoft.com/dotnet/8.0-bookworm-slim
EXPOSE 7001/TCP
WORKDIR /app/
COPY out ./

ENTRYPOINT ["./PaymentService"]