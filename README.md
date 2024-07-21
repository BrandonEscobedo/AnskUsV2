# Anskus
![Static Badge](https://img.shields.io/badge/Version-2-green)
## Tecnologias , librerias y Frameworks

-Blazor WebAssembly .NET 8

-Web Api .NET 8

-SignalR

-Entity Framework 8

-Fluent Validation

-AutoMapper

-MudBlazor

-MongoDb

-Sql Server 2019

-Azure Blob Storage

## Caracteristicas

Arquitectura limpia 

comunicacion WebSocket con SignalR

Autorizacion JWT

Minimal API 

## Enlace a sitio 
https://polite-dune-0f830c70f.5.azurestaticapps.net

Si la barra de carga no termina la Api esta inactiva , esperar y reiniciar cliente si es necesario.

usuario default:

Correo: userexample@example.com

Contraseña: dr&z10Md1$

## Diagramas de arquitectura 

### Crear Cuestionario

La solicitud Post a la Api se envia solo una vez al crear el cuestionario, dentro del nuevo cuestionario se aplica solo PUT sobre el mismo en las siguientes solicitudes

![](https://github.com/BrandonEscobedo/AnskUsV2/blob/main/img/DiagramaCrearCuestionario.png)

### Activar Cuestionario

![](https://github.com/BrandonEscobedo/AnskUsV2/blob/main/img/DiagramaActivarCuestionario.png)

### Ingresar a sala

![](https://github.com/BrandonEscobedo/AnskUsV2/blob/main/img/DiagramaEntrarSala.png)

## Autorización

El usuario autorizado tiene un JWT que se aloga en el LocalStorage del navegador y se envia en el encabezado de la solicitud.
Para Crear, Actualizar,Eliminar y Leer un cuestionario se necesita autorización.
para Entrar a un cuestionario activo no se necesita autorización.
