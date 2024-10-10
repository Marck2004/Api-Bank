En este apartado se va a explicar como se ha generado el Token JWT para la aplicacion del banco.

En primer lugar cuando accedamos al program.cs, vamos a añadir la autenticacion con sus parametros.
![[Pasted image 20240915122139.png]]
En el siguiente paso añadiremos la autorizacion, junto con el archivo que hemos creado para añadir al swagger la autorizacion.
![[Pasted image 20240915122245.png]]
Y por último lugar para este archivo, vamos a utilizar la autenticacion, la autorizacion y el mapeo de controllers, siempre va a ser en este orden:

1 - app.UseAuthentication
2 - app.UseAuthorization
3 - app.MapControllers

En el siguiente paso, vamos a ver como se crea el JWT en el servicio que en mi caso lo voy a llamar en el primer login solamente.
![[Pasted image 20240915122445.png]]
como podemos observar, se crean las claims que queremos que viajen en el token y se añade un json descriptor para adjuntar todas las caracteristicas de este, y al final de crear el token se devuelve en el servicio.

En el último paso vamos a ver como se ha modificado el swagger para que nos aparezca la autorizacion en este.
![[Pasted image 20240915122612.png]]
