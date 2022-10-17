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
dotnet ef migrations add Cleaning_Reservation --startup-project ../MySpot.Api/MySpot.Api.csproj --context MySpotDbContext -o ./DAL/Migrations
dotnet ef migrations add Introducing_Capacity --startup-project ../MySpot.Api/MySpot.Api.csproj --context MySpotDbContext -o ./DAL/Migrations

cd ../MySpot.Api
dotnet ef database update
```

### 
* https://datalust.co/seq
* http://localhost:5341/
```
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```