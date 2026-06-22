# ProductCatalogCleanApi

Web API en .NET 8 para un CRUD de productos usando arquitectura limpia, Entity Framework Core y SQL Server.

## Estructura

- `ProductCatalog.Domain`: entidad de dominio `Product`.
- `ProductCatalog.Application`: DTOs, interfaces y servicio de aplicacion.
- `ProductCatalog.Infrastructure`: `DbContext`, repositorio, configuracion de EF Core y migraciones.
- `ProductCatalog.API`: controladores, middleware centralizado de errores, DI y arranque.

## Base de datos

La cadena de conexion por defecto usa SQL Server LocalDB:

```json
"Server=(localdb)\\MSSQLLocalDB;Database=ProductCatalogDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

En Azure debes configurar una connection string llamada:

```text
ProductCatalogConnection
```

La aplicacion la lee con:

```csharp
configuration.GetConnectionString("ProductCatalogConnection")
```

## Migraciones automaticas

Al iniciar la API se ejecuta:

```csharp
await dbContext.Database.MigrateAsync();
```

Esto crea la base de datos y las tablas si no existen, y solo aplica migraciones pendientes si ya existe una base de datos previa.

## Ejecutar local sin Docker

Desde la carpeta raiz:

```powershell
dotnet restore
dotnet build
dotnet run --project ProductCatalog.API
```

## Ejecutar local con Docker

Construir la imagen:

```powershell
docker build -t productcatalog-api:local .
```

Ejecutar usando una cadena de conexion:

```powershell
docker run --rm -p 8080:8080 `
  -e ConnectionStrings__ProductCatalogConnection="Server=host.docker.internal;Database=ProductCatalogDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;Encrypt=False" `
  productcatalog-api:local
```

Probar:

```text
GET http://localhost:8080/api/products
```

## Despliegue Docker en Azure

Flujo recomendado:

1. Crear Resource Group.
2. Crear Azure SQL Server.
3. Crear Azure SQL Database `ProductCatalogDb`.
4. Crear Azure Container Registry.
5. Construir y subir la imagen Docker al registry.
6. Crear App Service usando contenedor Linux.
7. Configurar variables de entorno y cadena de conexion.
8. Probar `/api/products`.

Variables necesarias en App Service:

```text
WEBSITES_PORT=8080
ConnectionStrings__ProductCatalogConnection=Server=tcp:<sql-server>.database.windows.net,1433;Initial Catalog=ProductCatalogDb;Persist Security Info=False;User ID=<usuario>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

## Endpoints

- `GET /api/products`: lista productos.
- `GET /api/products/{id}`: obtiene un producto por ID.
- `POST /api/products`: crea un producto.
- `PUT /api/products/{id}`: actualiza un producto.
- `DELETE /api/products/{id}`: elimina un producto.

Ejemplo para crear:

```json
{
  "name": "Laptop",
  "description": "Laptop para desarrollo",
  "price": 1299.99,
  "stock": 10
}
```
