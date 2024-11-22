# InstitucionEducativaAPI

Este es un proyecto de API para gestionar información de estudiantes en una institución educativa. La API permite crear, leer, actualizar y eliminar registros de estudiantes, utilizando ASP.NET Core y Entity Framework Core para interactuar con una base de datos SQL Server.

## Descripción

El propósito de esta API es permitir la gestión de los datos de estudiantes de manera sencilla y eficiente. Los estudiantes tienen atributos como nombre, apellido, número de identificación y fecha de nacimiento. La API está construida con los siguientes componentes:

- **ASP.NET Core** para la construcción de la API.
- **Entity Framework Core** para el acceso y manejo de la base de datos.
- **SQL Server** como sistema de gestión de bases de datos.
- **Swagger** para documentar la API y facilitar la prueba de los endpoints.
- **Kestrel** como servidor web, configurado para usar HTTPS con un certificado.

## Requisitos

- **.NET 7.0 o superior** instalado en tu máquina.
- **SQL Server** o una base de datos compatible.
- Un **certificado SSL** (.pfx) para habilitar HTTPS en el servidor Kestrel (si se requiere).

## Instalación

### 1. Clona este repositorio

Primero, clona este repositorio en tu máquina local:

```bash
git clone https://github.com/tuusuario/InstitucionEducativaAPI.git
