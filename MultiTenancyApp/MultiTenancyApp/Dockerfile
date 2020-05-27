FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MultiTenancyApp/MultiTenancyApp.csproj", "MultiTenancyApp/"]
RUN dotnet restore "MultiTenancyApp/MultiTenancyApp.csproj"
COPY . .
WORKDIR "/src/MultiTenancyApp"
RUN dotnet build "MultiTenancyApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MultiTenancyApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MultiTenancyApp.dll"]