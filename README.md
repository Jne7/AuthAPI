#  API REST con Autenticación (ASP.NET Core)

##  Descripción
API REST desarrollada en ASP.NET Core que permite la autenticación de usuarios mediante login seguro. Implementa hashing de contraseñas y generación de tokens JWT

## Funcionalidades
- Registro de usuarios
- Inicio de sesión
- Hash de contraseñas para almacenamiento seguro
- Generación de token JWT al autenticarse
- Validación de usuarios mediante token

##  Tecnologías utilizadas
- ASP.NET Core Web API  
- C#  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  

##  Ejemplo de flujo
1. El usuario se registra  
2. La contraseña se guarda en forma de hash  
3. El usuario inicia sesión  
4. Si las credenciales son correctas, se genera un token JWT  
5. Ese token se usa para acceder a endpoints protegidos  

## Ejecutar el proyecto
1. Clonar el repositorio  
2. Abrir en Visual Studio 2022  
3. Configurar `appsettings.json`
4. Ejecutar migraciones si aplica:
   update-database
