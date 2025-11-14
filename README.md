# pruebaTecnicaCompraGamer
Prueba tecnica para compraGamer 11/2025
1) Instalación Docker
https://www.docker.com/products/docker-desktop/
docker de sql server
2) Imagen del motor SQL server en un contenedor
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=compraGamer432!" -p 1433:1433 --name sqlserver2022 -d mcr.microsoft.com/mssql/server:2022-latest
Instalar mssql o cualquier otro cliente
https://aka.ms/ssms/22/release/vs_SSMS.exe
backup preventivo
crear carpeta SQL_DB en C: para backups del snapshot del docker completo
si se usa otro volumen, cambiarlo en el archivo de backup y de restore
-------------------------------------------------------------------------------
3) BACKEND
-------------------------------------------------------------------------------
link local desde el depurador de VS 2022
http://localhost:5000/swagger/index.html
link desde el docker local del equipo de desarrollo
http://localhost:8080/
script de creación del docker de backend desde el Visual 2022, DESDE EL PRYECTO DEL API
docker build -f Dockerfile -t mibackend.api ..
docker run -d -p 8080:8080 --name mibackend mibackend.api
------------------------------------------------------
Migracion de la base de datos, DESDE EL PROYECTO DE DATA
dotnet ef migrations add Inicial --startup-project ../MiBackend.API
dotnet ef database update --startup-project ../MiBackend.API
4)FRONTEND
el puerto 8080 es del backend del docker, 
el puerto 5000 es el que levanta el VS
http://localhost:8081/
ese es el URL en el que queda la aplicacion totalmente dockerizada

esta instrucción compila el proyecto de front, crea un contenedor capaz de correr angular y realiza el deploy
docker compose up --build  
//////////Cambios para la rama DEV

//Implementacion de un feature_001





