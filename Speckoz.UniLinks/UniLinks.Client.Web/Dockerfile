# Estagio 1 - base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
RUN mkdir /app
WORKDIR /app
EXPOSE 80

# Estagio 2 - publish
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /app
COPY UniLinks.Client.Web/UniLinks.Client.Web.csproj .
COPY . .
RUN dotnet restore UniLinks.Client.Web/UniLinks.Client.Web.csproj
RUN dotnet publish UniLinks.Client.Web/UniLinks.Client.Web.csproj -c Release -o UniLinks.Client.Web/dist

# Estagio 3 - final
FROM base AS final
WORKDIR /dist
COPY --from=publish app/UniLinks.Client.Web/dist .
ENTRYPOINT [ "dotnet", "UniLinks.Client.Web.dll" ]