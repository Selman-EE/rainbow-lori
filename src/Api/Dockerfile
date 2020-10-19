FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY Api.csproj /build/
RUN dotnet restore ./build/Api.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./Api.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

FROM microsoft/dotnet:3.0-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "Api.dll"] 