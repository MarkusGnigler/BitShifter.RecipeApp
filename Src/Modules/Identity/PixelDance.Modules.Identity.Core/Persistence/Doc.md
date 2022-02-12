
#### Add Migration
dotnet ef migrations add InitialCrate -o Persistence/Migrations
dotnet ef database update

dotnet ef migrations add {migration_name} --startup-project ../../../Bootstrapper/PixelDance.Bootstrapper --context IdentityDbContext -o ./Persistence/Migrations
dotnet ef migrations add InitialCreate --startup-project ../../../Bootstrapper/PixelDance.Bootstrapper --context IdentityDbContext -o ./Persistence/Migrations

dotnet ef database update -s ../../../Bootstrapper\PixelDance.Bootstrapper --context IdentityDbContext