FROM mcr.microsoft.com/dotnet/aspnet:6.0

LABEL version="1.0" maintainer="Guilherme S. de Azevedo"

WORKDIR /app

COPY ./dist .

ENTRYPOINT ["dotnet", "Rest_API.dll"]

