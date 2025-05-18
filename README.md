# Restaurants API

## Configuración de la Base de Datos

Para gestionar las migraciones de la base de datos con Entity Framework, utiliza los siguientes comandos:

Antes de ejecutar los comandos, dirígete a la carpeta `Restaurants.Infrastructure:`

```sh
cd Restaurants.Infrastructure
```

### Agregar una nueva migración

```sh
dotnet ef migrations add <migration-name> --verbose --project Restaurants.Infrastructure.csproj --startup-project ../Restaurants.API/Restaurants.API.csproj
```

Reemplaza `<migration-name>` con un nombre descriptivo para la migración.

### Aplicar las migraciones

```sh
dotnet ef database update --verbose --project Restaurants.Infrastructure.csproj --startup-project ../Restaurants.API/Restaurants.API.csproj
```

Este comando actualizará la base de datos con las migraciones pendientes.
