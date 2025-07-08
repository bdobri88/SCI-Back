# Sistema de Consultas Informaticas (SCI)

Este proyecto es un sistema de gestión integral diseñado para administrar las consultas informáticas realizadas por toda la jurisdicción del centro de despacho. Su función principal es permitir una gestión eficiente de las consultas realizadas por las dependencias y el personal policial que lo requiera. Además, es una solución que simplifica la gestión de los efectivos de la oficina de estadísticas, ya que facilita la manipulación y organización de los datos. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre los datos de efectivos y dependencias, facilitando la organización y consulta de la estructura de personal.

## Tabla de Contenidos

1.  [Descripción General del Sistema](#1-descripción-general-del-sistema)
2.  [Arquitectura](#2-arquitectura)
3.  [Tecnologías Utilizadas](#3-tecnologías-utilizadas)
4.  [Configuración del Entorno de Desarrollo](#4-configuración-del-entorno-de-desarrollo)
    * [Prerrequisitos](#prerrequisitos)
    * [Estructura de Carpetas](#estructura-de-carpetas)
    * [Configuración de la Base de Datos (SQL Server)](#configuración-de-la-base-de-datos-sql-server)
    * [Configuración del Backend (API .NET)](#configuración-del-backend-api-net)
    * [Configuración del Frontend (Angular)](#configuración-del-frontend-angular)
5.  [Ejecución del Proyecto](#5-ejecución-del-proyecto)
    * [Levantar todos los servicios con Docker Compose](#levantar-todos-los-servicios-con-docker-compose)
    * [URLs de Acceso](#urls-de-acceso)


## 1. Descripción General del Sistema

El Sistema de Consultas Informáticas (`SCI`) se enfoca en proporcionar una herramienta eficiente para la gestión de la información relacionada con el personal y la estructura departamental. Sus funcionalidades clave incluyen:

* **Registro de Consultas Informáticas:** Permite el registro detallado de consultas sobre automotores, personas y armas, incluyendo la verificación de impedimentos legales.
* **Gestión y Extracción de Consultas:** Facilita la visualización y extracción de información referente a las consultas informáticas realizadas de forma diaria o mensual.
* **Gestión de Efectivos:** Permite el alta, baja, modificación y consulta de la información detallada de cada efectivo (legajo, nombre, apellido, dependencia asignada).
* **Gestión de Usuarios:** Permite la administración de usuarios del sistema, incluyendo su ID, contraseña, asociación con un efectivo y rol (administrador o no).
* **Gestión de Dependencias:** Habilita la creación, edición y eliminación de dependencias o departamentos, con la posibilidad de asignar efectivos a las mismas.
* **Listado y Búsqueda:** Ofrece la visualización de efectivos y dependencias en tablas paginadas y filtrables para una fácil localización de la información.
* **Autenticación de Usuarios:** Garantiza el acceso seguro al sistema mediante un proceso de inicio de sesión con tokens JWT.

El objetivo principal es optimizar la administración de consultas informaticas, asegurar la integridad de los datos y facilitar la toma de decisiones basada en la organización de la fuerza laboral.

## 2. Arquitectura

Este proyecto implementa una arquitectura **Cliente-Servidor (Client-Server)** con una API RESTful. El Frontend y el Backend están completamente desacoplados, lo que permite una mayor flexibilidad, escalabilidad y facilidad de mantenimiento.

* **Frontend (Cliente):** Una aplicación de una sola página (SPA) desarrollada con Angular. Es responsable de la interfaz de usuario, la lógica de presentación y la interacción directa con el usuario final. Se comunica con el Backend a través de solicitudes HTTP.
* **Backend (Servidor):** Una API RESTful construida con ASP.NET Core. Maneja la lógica de negocio, la autenticación y la interacción con la base de datos. Expone endpoints bien definidos para que el Frontend pueda consumir los datos y funcionalidades.
* **Base de Datos:** Un servidor SQL Server para el almacenamiento persistente de todos los datos del sistema.
* **Contenerización:** Se utiliza Docker y Docker Compose para empaquetar y orquestar todos los componentes (Base de Datos, Backend y Frontend), asegurando un entorno de desarrollo consistente y facilitando el despliegue.

## 3. Tecnologías Utilizadas

Lista detallada de las tecnologías y herramientas principales empleadas en el desarrollo del sistema.

* **Backend:**
    * **Lenguaje:** C#
    * **Framework:** .NET 8 (ASP.NET Core Web API)
    * **ORM:** Entity Framework Core
    * **Base de Datos:** SQL Server
    * **Autenticación:** JSON Web Tokens (JWT)
    * **Documentación API:** Swagger / OpenAPI
* **Frontend:**
    * **Framework:** Angular 14
    * **Lenguaje:** TypeScript
    * **Componentes UI:** Angular Material
    * **Manejo de estados/asincronía:** RxJS
    * **Servidor web (para el contenedor):** Nginx
* **Herramientas de Contenerización:**
    * **Motor:** Docker Engine
    * **Orquestación:** Docker Compose

## 4. Configuración del Entorno de Desarrollo

### Prerrequisitos

Asegúrate de tener instalado el siguiente software en tu máquina de desarrollo:

* **Git:** Para clonar el repositorio.
    * [Descargar Git](https://git-scm.com/downloads)
* **Docker Desktop:** Incluye Docker Engine y Docker Compose, esenciales para levantar el entorno completo.
    * [Descargar Docker Desktop](https://www.docker.com/products/docker-desktop/)


### Configuración de la Base de Datos (SQL Server)

* La base de datos SQL Server se ejecutará como un contenedor de Docker.
* No se requiere instalación manual de SQL Server en tu máquina host.
* Los datos de la base de datos se persistirán en un volumen Docker llamado `sql_server_data`, garantizando que tus datos no se pierdan al detener/reiniciar los contenedores.
* Las credenciales y la configuración inicial se definen en el `docker-compose.yml`.

### Configuración del Backend (API .NET)

* **Ubicación:** `SCI-Back/`
* **Conexión a la Base de Datos:** La cadena de conexión a SQL Server (`ConnectionStrings__defaultConnection`) está configurada en `docker-compose.yml` para usar el nombre del servicio de la base de datos (`sql-server`) dentro de la red Docker.
* **Configuración de JWT:** Las claves secretas, el emisor (`AppSettings__Issuer`) y la audiencia (`AppSettings__Audience`) para los tokens JWT también se configuran a través de variables de entorno en `docker-compose.yml`. El emisor (`http://api`) y la audiencia (`http://frontend-service`) son los nombres de los servicios dentro de la red Docker.
* **CORS:** La API está configurada para permitir solicitudes CORS desde `http://localhost:4200`. Esta configuración se encuentra en el archivo `Program.cs` del proyecto del backend.

### Configuración del Frontend (Angular)

* **Ubicación:** `SCI-Front/app/`
* **URL de la API:** La URL a la que el frontend se conecta para interactuar con la API está definida en el archivo de entorno de producción de Angular:
    * `SCI-Front/app/src/environments/environment.prod.ts`
    * Para desarrollo local con Docker Compose:    
  
        export const environment = {
          production: true,
          apiUrl: 'http://localhost:8080/api' 
        };

## 5. Ejecución del Proyecto

### Levantar todos los servicios con Docker Compose

1.  Abre tu terminal (PowerShell, CMD, Git Bash, WSL).
2.  Navega a la carpeta donde se encuentra el archivo `docker-compose.yml`. Al clonar el repositorio la el archivo esta en la raiz `cd SCI-Back`.

3.  Ejecuta el siguiente comando para construir las imágenes de Docker y levantar todos los servicios en segundo plano:

    docker-compose up --build -d

    * La primera vez, esto descargará las imágenes base y construirá tu API y Frontend, lo cual puede tardar varios minutos.
    * El flag `--build` fuerza la reconstrucción de las imágenes. Úsalo siempre que hayas realizado cambios en el código del frontend, backend, o en los Dockerfiles.
    * El flag `-d` (detached mode) ejecuta los contenedores en segundo plano, liberando tu terminal.

4.  Para verificar que los servicios se estén ejecutando correctamente:

    docker-compose ps

    Deberías ver `sql-server`, `api`, y `frontend-service` con estado `Up`.

5.  Para detener todos los servicios y eliminar los contenedores, redes y volúmenes (excepto los volúmenes nombrados, como `sql_server_data` que persisten los datos de la DB):

    docker-compose down
 

### URLs de Acceso

Una vez que todos los servicios estén levantados y ejecutándose:

* **Frontend (Aplicación Angular):** Abre tu navegador web y navega a:
    `http://localhost:4200`
* **Backend (API .NET):** Para probar los endpoints directamente, o si necesitas verificar que la API esté corriendo:
    `http://localhost:8080`
* **Swagger UI (Documentación interactiva de la API):** Accede a la documentación de tu API para probar los endpoints:
    `http://localhost:8080/swagger`

