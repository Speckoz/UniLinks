# Estagio 1 - base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
RUN mkdir /app
WORKDIR /app
EXPOSE 80/tcp

# Estagio 2 - publish
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /app
COPY UniLinks.API/UniLinks.API.csproj .
COPY . .
RUN dotnet restore UniLinks.API/UniLinks.API.csproj
RUN dotnet publish UniLinks.API/UniLinks.API.csproj -c Release -o UniLinks.API/dist

# Estagio 3 - final
FROM base AS final
WORKDIR /dist
COPY --from=publish app/UniLinks.API/dist .
ENTRYPOINT [ "dotnet", "UniLinks.API.dll" ]