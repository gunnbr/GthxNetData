# GthxNetData
Data component shared between the gthx bot and website.

# Data updates
To make database changes either:
1. Modify the classes in Gthx.Core
2. Add or delete classes in Gthx.Core, then modify GthxDataContext to 
add or remove references to those classes.

Then create new migrations for both MariaDB and SQL Server by running 
the following from the Gthx.Data directory:

```dotnet ef migrations add <MigrationName> --project ../SqlServerMigrations -- --provider sqlserver
dotnet ef migrations add <MigrationName> --project ../MariaDbMigrations -- --provider mariadb
```
