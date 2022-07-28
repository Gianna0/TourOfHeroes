FROM golang:1.18 AS builder
WORKDIR /build
COPY ./hero-go/ .
RUN go get -d -v

RUN CGO_ENABLED=1 GOOS=linux go build -o out -a -ldflags '-linkmode external -extldflags "-static"' .

FROM scratch
COPY --from=builder /build/out /app
COPY ./hero-go/configuration.json .
COPY ./hero-go/wwwroot ./wwwroot

EXPOSE 5000
ENTRYPOINT [ "/app" ]