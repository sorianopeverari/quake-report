FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ["QuakeReport.App/QuakeReport.App.Console/QuakeReport.App.Console.csproj", "QuakeReport.App/QuakeReport.App.Console/"]
COPY ["QuakeReport.Domain/QuakeReport.Domain.Models/QuakeReport.Domain.Models.csproj", "QuakeReport.Domain/QuakeReport.Domain.Models/"]
COPY ["QuakeReport.Domain/QuakeReport.Domain.Business/QuakeReport.Domain.Business.csproj", "QuakeReport.Domain/QuakeReport.Domain.Business/"]
COPY ["QuakeReport.Domain/QuakeReport.Domain.Repositories/QuakeReport.Domain.Repositories.csproj", "QuakeReport.Domain/QuakeReport.Domain.Repositories/"]
COPY ["QuakeReport.Infra/QuakeReport.Infra.FileRepository/QuakeReport.Infra.FileRepository.csproj", "QuakeReport.Infra/QuakeReport.Infra.FileRepository/"]
COPY ["QuakeReport.Tests/QuakeReport.Tests.Unit/QuakeReport.Tests.Unit.csproj", "QuakeReport.Tests/QuakeReport.Tests.Unit/"]

RUN dotnet restore "QuakeReport.App/QuakeReport.App.Console/QuakeReport.App.Console.csproj"
COPY . .
WORKDIR "/src/QuakeReport.App/QuakeReport.App.Console"
RUN dotnet build "QuakeReport.App.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuakeReport.App.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuakeReport.App.Console.dll"]
