# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY OdeToFood/*.csproj ./OdeToFood/
COPY OdeToFood.Core/*.csproj ./OdeToFood.Core/
COPY OdeToFood.Data/*.csproj ./OdeToFood.Data/
COPY OdeToFood.CLI/*.csproj ./OdeToFood.CLI/
RUN dotnet restore -v normal -r linux-musl-x64

# copy everything else and build app
COPY OdeToFood/. ./OdeToFood/
COPY OdeToFood.Core/. ./OdeToFood.Core/
COPY OdeToFood.Data/. ./OdeToFood.Data/
COPY OdeToFood.CLI/. ./OdeToFood.CLI/
WORKDIR /source/OdeToFood

RUN curl -sL https://deb.nodesource.com/setup_13.x | bash - \
&& apt-get install -y nodejs \
&& rm -rf /var/lib/apt/lists/*

RUN dotnet publish -c release \
                -o /app \
                -r linux-musl-x64 \
                --self-contained false \
                --no-restore

WORKDIR /app
RUN npm install

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
#RUN apk add --no-cache icu-libs
#ENV LC_ALL en_US.UTF-8
#ENV LANG en_US.UTF-8

ENTRYPOINT ["./OdeToFood"]
