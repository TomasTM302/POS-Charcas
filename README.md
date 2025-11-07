# POS Charcas

Estructura inicial para una aplicación de punto de venta (POS) desarrollada con C#, WinForms y SQL Server utilizando Entity Framework Core. El código está organizado en capas para separar responsabilidades entre presentación, negocio y acceso a datos.

## Proyectos

- `POSCharcas.Presentation`: interfaz de usuario en WinForms con formularios para login, panel principal, ventas, productos y clientes.
- `POSCharcas.Business`: contiene la lógica de negocio, modelos de transferencia y servicios que coordinan la interacción con los repositorios.
- `POSCharcas.Data`: capa de acceso a datos basada en Entity Framework Core, con entidades, contexto y repositorios.

## Configuración

El archivo `appsettings.json` (en la capa de presentación) incluye una cadena de conexión de ejemplo que puede adaptarse a la configuración de SQL Server utilizada en el entorno de desarrollo.

## Próximos pasos

- Implementar migraciones de Entity Framework para crear la base de datos.
- Sustituir los llamados síncronos a la base de datos utilizados durante el prototipo por versiones asíncronas donde corresponda.
- Agregar pruebas unitarias y de integración para los servicios de negocio.
