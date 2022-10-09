# MySpot

```
dotnet new --list
dotnet new xunit -n MySpot.Tests.Unit
dotnet sln add tests/MySpot.Tests.Unit/MySpot.Tests.Unit.csproj
```

## 8.6
```
dotnet new classlib -n MySpot.Core
dotnet sln add src/MySpot.Core/MySpot.Core.csproj

```

```
docker compose up -d
docker ps
docker logs postgres
docker compose down
```

```
dotnet ef
dotnet ef database

dotnet ef migrations
cd src/MySpot.Infrastructure
dotnet ef migrations add Init -o ./DAL/Migrations --startup-project ../MySpot.Api

cd ../MySpot.Api
dotnet ef database update
```