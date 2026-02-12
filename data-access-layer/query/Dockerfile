# Stage 1: Build
FROM golang:1.25-alpine3.23 AS build
WORKDIR /app
COPY go.mod go.sum ./
RUN go mod download
COPY . .
RUN CGO_ENABLED=0 GOOS=linux go build -o dal-query-service ./cmd/server/main.go

# Stage 2: Final Run
FROM alpine:3.23
WORKDIR /root/
COPY --from=build /app/dal-query-service .
RUN wget -qO/bin/grpc_health_probe https://github.com/grpc-ecosystem/grpc-health-probe/releases/download/v0.4.18/grpc_health_probe-linux-amd64 && \
    chmod +x /bin/grpc_health_probe

EXPOSE 50051
CMD ["./dal-query-service"]