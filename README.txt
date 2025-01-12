
# Proyecto API Usuarios Tareas

## Descripción
Este proyecto es una API RESTful que gestiona tareas de usuarios, incluyendo autenticación, creación, obtención, filtrado y paginación de tareas.

## Requisitos
- .NET 8 SDK o superior
- SQL Server o base de datos compatible
- Herramienta para ejecutar migraciones (como Visual Studio o la CLI de .NET)

## Pasos para configurar y ejecutar el proyecto

### 1. Clonar el repositorio
Clona el repositorio en tu máquina local:

```
git clone https://github.com/David-JKLIL/APIUsuariosTareas.git
cd APIUsuariosTareas
```

### 2. Configurar la base de datos
Asegúrate de tener una instancia de SQL Server corriendo.

1. Crea una base de datos (por ejemplo, `TareasDB`).
2. En el archivo `appsettings.json`, configura la cadena de conexión a tu base de datos:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TareasDB;Trusted_Connection=True;"
}
```

Reemplaza `localhost` con el nombre de tu servidor de base de datos si es necesario.

### 3. Aplicar las migraciones
Si es la primera vez que ejecutas el proyecto, necesitarás aplicar las migraciones para crear las tablas en la base de datos. Si no tienes las herramientas de EF Core, instálalas con el siguiente comando:

```bash
dotnet tool install --global dotnet-ef
```

Luego, ejecuta las migraciones:

```bash
dotnet ef database update
```

> **Importante:** Esta aplicación está basada en el enfoque "Code First", lo que significa que las migraciones se aplican automáticamente a la base de datos.

### 4. Ejecutar el proyecto
Puedes ejecutar el proyecto usando Visual Studio o la CLI de .NET:

```
dotnet run
```

La API estará disponible en `http://localhost:5000` (o el puerto configurado).

## Instrucciones para configurar JWT

1. En el archivo `appsettings.json`, agrega la clave secreta para la generación del token JWT:

```json
"JWT": {
  "Key": "tu_clave_secreta_aqui",
  "Issuer": "mi_issuer",
  "Audience": "mi_audience"
}
```

2. **Generar un token JWT:** Puedes usar herramientas como Postman o cualquier cliente HTTP para autenticarte y obtener un token JWT enviando las credenciales del usuario.

### Swagger
La aplicación incluye Swagger para que puedas probar la API. Accede a `http://localhost:5000/swagger` para ver las rutas disponibles y realizar pruebas con la API.

## Detalles adicionales

### Autenticación
La API utiliza JWT para la autenticación. Asegúrate de incluir el token en el encabezado `Authorization` de cada solicitud.

### Pruebas unitarias
Puedes ejecutar las pruebas unitarias con el siguiente comando:

```
dotnet test
```
