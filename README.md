# BankingApp
## Instrucciones para ejecutar localmente
1. Clonar el proyecto
`git clone https://github.com/lebarquero/BankingApp.git`
2. Crear la BD BankingDB
Navegar a la carpeta _BankinAPI_ en una terminal y ejecutar:
`dotnet ef database update`
O ejecutar el script _CreateBankingDB.sql_ que se encuentra en la carpeta _Scripts_ del repositorio
3. Ejecutar el proyecto
Navegar a la carpeta _BankinAPI_ en una terminal y ejecutar:
`dotnet run`
O abrir el archivo de solución _BankingApp.sln_ in Visual Studio y presionar <F5>
4. Probar los endpoints
En la carpeta _EndpointsData_ del repositorio se encuentran los archivos json de Postman
## Breve Descripción
La solución cuenta con los siguiente proyectos:
1. **Banking.Business**
   Es un proyecto de tipo Class Library, el cual contiene los servicios de lógica de negocio.
2. **Banking.DataAccess**
   Es un proyecto de tipo Class Library, el cual contiene los elementos requeridos para la persistencia y acceso a los datos.
3. **Banking.Entities**
   Es un proyecto de tipo Class Library, el cual contiene las entidades del dominio de negocio
4. **BankingAPI**
   Es un proyecto de tipo ASP .NET WebAPI, el cual contiene los contralodres que manejan las peticiones de los usuarios.
