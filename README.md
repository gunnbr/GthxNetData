# GthxNetData
Data component shared between the gthx bot and website.

# Data updates
To make database changes either:
1. Modify the classes in Gthx.Core
2. Add or delete classes in Gthx.Core, then modify GthxDataContext to 
add or remove references to those classes.

Then create a new SQL Server migration by running the following from the
repository root (the folder with `Gthx.Data.sln`):

```
dotnet ef migrations add <MigrationName> --project SqlServerMigrations --startup-project SqlServerMigrations
```

This needs the EF Core 10 tools: install them with `dotnet tool update --global dotnet-ef`,
or run them without installing as `dnx dotnet-ef@10.0.14 migrations add ...`. Check that
the new migration only has the changes you meant, and that the website's queries still work
(GthxNetBot's `GthxWebDataTest` covers them).

`SqlServerMigrations` references `Microsoft.EntityFrameworkCore.Tools` (and so the Design
package the tools need), and `Gthx.Data` references `Microsoft.EntityFrameworkCore.SqlServer`.
GthxNetBot uses the same EF Core packages: when one repository updates EF Core, update the
other to the same version.
