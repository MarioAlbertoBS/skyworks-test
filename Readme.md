## Skyworks Test

Exámen de prueba para Skyworks

### Pasos

- Copia `appsettings.Example.json` a `appsettings.Development.json` y `appsettings.json`.
- Llena el campo `DefaultConnection` con la Connection String de SQL Server.
- Ejectuta las migraciones (Requiere Entity Framework Tools) `dotnet ef database update`.
- Ejectuta la app `dotnet run`.
