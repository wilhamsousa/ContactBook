FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Uex.ContactBook.Api/Uex.ContactBook.Api.csproj", "Uex.ContactBook.Api/"]
COPY ["Uex.ContactBook.Application/Uex.ContactBook.Application.csproj", "Uex.ContactBook.Application/"]
COPY ["Uex.ContactBook.Domain/Uex.ContactBook.Domain.csproj", "Uex.ContactBook.Domain/"]
COPY ["Uex.ContactBook.Infra/Uex.ContactBook.Infra.csproj", "Uex.ContactBook.Infra/"]
RUN dotnet restore "Uex.ContactBook.Api/Uex.ContactBook.Api.csproj"
COPY . .
WORKDIR "/src/Uex.ContactBook.Api"
RUN dotnet build "Uex.ContactBook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Uex.ContactBook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_HTTP_PORTS=8080

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uex.ContactBook.Api.dll"]
