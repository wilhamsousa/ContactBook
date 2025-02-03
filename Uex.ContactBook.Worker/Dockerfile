FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Uex.ContactBook.Worker/Uex.ContactBook.Worker.csproj", "Uex.ContactBook.Worker/"]
COPY ["Uex.ContactBook.Domain/Uex.ContactBook.Domain.csproj", "Uex.ContactBook.Domain/"]
COPY ["Uex.ContactBook.Infra/Uex.ContactBook.Infra.csproj", "Uex.ContactBook.Infra/"]
RUN dotnet restore "Uex.ContactBook.Worker/Uex.ContactBook.Worker.csproj"
COPY . .
WORKDIR "/src/Uex.ContactBook.Worker"
RUN dotnet build "Uex.ContactBook.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Uex.ContactBook.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8085:8085
EXPOSE 8380:8380

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_HTTP_PORTS=8085

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uex.ContactBook.Worker.dll"]