FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /apprelease
COPY /hero-csharp/Application/*.csproj .
RUN dotnet restore hero-csharp.csproj
COPY ./hero-csharp .
RUN dotnet publish Application/hero-csharp.csproj -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /apprelease/out .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT dotnet hero-csharp.dll