# Estagio 1 - base
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN mkdir /app
WORKDIR /app
EXPOSE 80

# Estagio 2 - publish
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /app
COPY UniLinks.Client.Site/UniLinks.Client.Site.csproj .
COPY . .
RUN dotnet restore UniLinks.Client.Site/UniLinks.Client.Site.csproj
RUN dotnet publish UniLinks.Client.Site/UniLinks.Client.Site.csproj -c Release -o UniLinks.Client.Site/dist

# Estagio 3 - final
FROM base AS final
WORKDIR /dist
COPY --from=publish app/UniLinks.Client.Site/dist .
ENTRYPOINT [ "dotnet", "UniLinks.Client.Site.dll" ]