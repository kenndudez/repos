FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BaseProjects/BaseProjects.csproj", "BaseProjects/"]
RUN dotnet restore "BaseProjects/BaseProjects.csproj"
COPY . .
WORKDIR "/src/BaseProjects"
RUN dotnet build "BaseProjects.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BaseProjects.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BaseProjects.dll"]