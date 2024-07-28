Primera Tarea de Algoritmos y Estructura de Datos 1
Estudiante:
    Nombre: Eduardo José Canessa Quesada
    Carné: 2024073704

Se utilizó una aplicación tipo consola con .NET 8 y C#.
El código se realizó en VS Code.

Indicaciones para iniciar el programa correctamente.
    -Se debe correr en la terminal del sistema operativo. En mi caso, daré un ejemplo el cual es para Windows 11. Se abre terminal del SO con 
    el botón de windos junto con "R" y se escribe "cmd" en el ejecutor.

        C:\Users> dotnet run --project "C:\User\Desktop\TareaDatos1\Tarea1_Datos1_Eudardo_Canessa.csproj" -puerto

        En este ejemplo "C:\User\Desktop\TareaDatos1\" debe ser reemplazado por la dirección de carpeta donde se encuentre "Tarea1_Datos1_Eudardo_Canessa.csproj"
        En "-puerto" solo la palabra puerto debe ser reemplazada por un número como por ejemplo "-4404"
    
    -Si se inicia por primera vez, se ejecutará el servidor en el puerto indicado y se mostrará en consola un indicador correspondiente.
    -Si se inicia el código en otras terminales, se ejecutará como cliente del mismo puerto del servidor iniciado anteriormente y se mostrará un indicador de cliente.
    -Se pueden conectar un máximo de 5 clientes, esto se puede modificar en la línea 25 de "ServerClass".
    -Una vez se tengan un par de clientes como mínimo, se puede escribir un mensaje por terminal en cualquiera de ellos y al oprimir "Enter", este mensaje será recibido por los demás
    clientes conectados al puerto del servidor.
    