#  Million Backend

Backend del sistema **Million**, una API RESTful desarrollada en **.NET 8** con arquitectura por capas (Domain, Application, Infrastructure, Api y Tests).  
El proyecto está diseñado para gestionar propiedades inmobiliarias, integrando persistencia en MongoDB y almacenamiento de imágenes en AWS S3.

---

##  Tecnologías utilizadas

- **.NET 8 (C#)**
- **ASP.NET Core Web API**
- **MongoDB.Driver**
- **AWS SDK for .NET (S3)**
- **Swagger / OpenAPI**
- **NUnit + Moq** (para pruebas unitarias)

---

##  Estructura del Proyecto
Million.Backend/
│
├── Million.Api/ # Capa de presentación (API REST)
│ ├── Controllers/ # Controladores HTTP
│ │ └── PropertiesController.cs
│ ├── Program.cs # Configuración principal y endpoints
│ └── appsettings.json # Configuración general
│
├── Million.Application/ # Lógica de negocio / servicios
│ ├── DTOs/ # Objetos de transferencia (Create, Update)
│ └── Services/ # Servicios (PropertyService)
│
├── Million.Domain/ # Entidades de dominio y configuración
│ ├── Entities/ # Clases como Property.cs
│ └── Configuration/ # Configuración de MongoDB
│
├── Million.Infrastructure/ # Acceso a datos (MongoDB, repositorios)
│ ├── Database/ # Contexto de MongoDB
│ └── Repositories/ # Implementaciones de repositorios
│
├── Million.Tests/ # Pruebas unitarias (NUnit)
│ └── PropertyServiceTests.cs
│
└── scripts/ # Scripts de inicialización o carga
└── seed_million.js


Este proyecto sigue la arquitectura **Clean Architecture / Onion Architecture**, separando responsabilidades en capas:

| Capa               | Descripción |
|--------------------|-------------|
| **Domain**         | Define las entidades base y modelos del negocio. |
| **Application**    | Contiene la lógica de negocio y servicios. |
| **Infrastructure** | Acceso a datos y configuración de MongoDB. |
| **API**            | Exposición de endpoints REST. |
| **Tests**          | Pruebas unitarias para servicios. |

Compilar el proyecto -> dotnet build
Limpiar archivos de compilación -> dotnet clean
Ejecutar la API -> dotnet run -p .\Million.Api\Million.Api.csproj
Ejecutar pruebas unitarias -> dotnet test


restaurar base de datos 

mongorestore --uri="mongodb://localhost:27017/" `
  --gzip --archive="backup_MillionDB_20251015-190147.gz" `
  --nsFrom="MillionDB.*" --nsTo="MillionDB_copia.*" --drop

copia -> backup_MillionDB_20251015-190147.gz