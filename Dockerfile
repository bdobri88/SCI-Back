# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WSInformatica/WSInformatica.csproj", "WSInformatica/"]
RUN dotnet restore "WSInformatica/WSInformatica.csproj"

COPY . .
WORKDIR "/src/WSInformatica"
RUN dotnet build "WSInformatica.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 80 
EXPOSE 443 

ENTRYPOINT ["dotnet", "WSInformatica.dll"] 