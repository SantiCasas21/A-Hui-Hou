# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar archivos de solución y proyectos para restaurar dependencias
COPY *.slnx .
COPY src/AHuiHou.Domain/*.csproj src/AHuiHou.Domain/
COPY src/AHuiHou.Application/*.csproj src/AHuiHou.Application/
COPY src/AHuiHou.Infrastructure/*.csproj src/AHuiHou.Infrastructure/
COPY src/AHuiHou.Api/*.csproj src/AHuiHou.Api/
RUN dotnet restore AHuiHou.slnx

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish src/AHuiHou.Api/AHuiHou.Api.csproj -c Release -o /out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .

# Exponer el puerto de la API
EXPOSE 8080
ENTRYPOINT ["dotnet", "AHuiHou.Api.dll"]
