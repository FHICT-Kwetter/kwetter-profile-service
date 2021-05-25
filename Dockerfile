# workaround for https://github.com/grpc/grpc/issues/24153
RUN apt-get update && apt-get install -y libc-dev && apt-get clean

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ProfileService.Api/ProfileService.Api.csproj", "ProfileService.Api/"]
COPY ["ProfileService.Data/ProfileService.Data.csproj", "ProfileService.Data/"]
COPY ["ProfileService.Domain/ProfileService.Domain.csproj", "ProfileService.Domain/"]
COPY ["ProfileService.Service/ProfileService.Service.csproj", "ProfileService.Service/"]
RUN dotnet restore "ProfileService.Api/ProfileService.Api.csproj"
COPY . .
WORKDIR /src/ProfileService.Api
RUN dotnet build "ProfileService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileService.Api.dll"]