# Async/Await/GetAwaiter (.NET)

[Basado en el artículo relativo a Asyn/Await de microsoft ](https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/async/)
uno encuentra este esquema donde se ilustra 
cómo funciona las keys Async y Await.

![alt text](img/msdnAsyncpicture.png)

Podemos ver que se ejecuta dentro de un método 'async' para acceder asincronamente al contenido de una URL por http. Los siguientes pasos se ejecutan durante su ejecución:

1) la descarga de una pagina por http. Aunque el método es Async, la key 'await' no se coloca en dicha línea.

2) Esto hace que la ejecución se haga en paralelo de un trabajo "DoIndependentWork()" 

3) Luego se hace una espera usando la key 'await' sobre la task creada para la descarga.

4) una vez la task es completada, se devuelve el tamaño de la página.

# Y las dudas surgieron..

Siguiendo ese esquema, surgieron dudas como:

¿cómo es realmente su performance?
¿Cúantos hilos?¿Cómo avanza la ejecución del DoIndependentWork con respecto al método Asíncrono?

¿cómo seria el performance si usamos el GetAwaiter y/o GetResult de la clase Task en vez de usar la key 'await'?

En el ejemplo entendí que habia cierto avance en paralelo entre la descarga web y el DoIndependentWork, pero..  cuando éste último termina y se hace la espera de la task ¿ el método invocante a dicho método  (en otras palabras, sería el que está por encima en la pila de llamadas) AccessTheWebAsync.. también estaría progresando su ejecución?

¿Puede haber deadlock en algún contexto si se mezclan llamadas await y GetAwaiter? ¿Que pasaría si un metodo que devuelve una Task y se le invocó con un Getwaiter..  al tiempo se le añadiera la key Async a dicha implementación?

Entonces,.. ante estas dudas y otras que han ido surgiendo.. implementé esta aplicación a modo de prueba de concepto. 

Y para terminar esta introducción, cosas que me llamaron la atención a vuela pluma,.. 

1) no se puede definir interfaces con la key "async" al menos que yo sepa. El compilador no lo permite, pero sin embargo, a la hora de implementarlo se puede elegir entre añadirse o no.. Con lo cual, pienso que el convenio de marcar los métodos con el sufijo Async, se me antoja más una necesidad que un consejo. 

2) por otro lado, si tenemos un método con su signatura Async ( y el nombre del método bien sufijado ) pero su cuerpo no contiene ningún await, en dicho caso se comportaría de manera asíncrona. ¿Qué pasaría si el await pese a existir en dicho método, estuviera sujeto a la condicón de un if y ésta no se diera?¿Cuál sería el comportamiento?

3) por último y no menos importante, mpresionante documentación del método GetAwaiter. No se puede ser más escueto. https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.getawaiter?view=netframework-4.7.2 


# ¿En que consiste la prueba de concepto?

Inspirado en el ejemplo de Microsoft relatado al principio de este artículo,.. tenemos las siguientes funciones o roles GetStringAsync y DoIndependentWork. 


## Rol de GetStringAsync ##

Este procedimiento asíncrono se invoca 
de tal manera que no haya bloqueo sobre el hilo invocante, el cual en vez de esto, facilita el progreso de la ejecución del método DoIndependentWork.

En la prueba de concepto se va a sustituir por la llamada a un método asyncrono (o su versión no asyncrona si procede), el cual deberá devolver una cadena de texto ( en el ejemplo original, se descarga una página web). Para que dicha llamada asíncrona tuviera más sentido, se introducirán los elementos necesarios para provocar un efecto "bloqueo" que haga que avance los métodos DoIndependentWork.

Resumiendo, este rol serán sustituidos por dos interfaces.

    1) GetStringAsync()
    2) GetString()

## Rol de DoIndependentWork ##

Cada método compondrá su propia cadena la cual formára parte parcial de la cadena devuelta a niveles superiores. 

La idea es ver como impacta al performance de los async en los casos que:

1) si se hace un uso más o menos intensivo de la CPU

2) si se hace de manera Async o no

Durante la creación de la cadena, se van a generar los puntos de las series que más adelante se mostraran en la gráfica.

Dependiendo de la combinación de los puntos 1) y 2) la concurrencia a los métodos que generan la gráfica final tandrán un acceso serializado. Por eso, la gráfica será tratada como un recurso compartido.  

## Resumiendo y  al grano.. ##

La aplicación definirá una cascada de métodos  de varios niveles.

Se podrá elegir por cada nivel o método, 

1) Si va a ser implementado con Async o no

2) Número de iteraciones o milisegundos a simular dependiendo si se elige Looping o Sleeping

3) También se va a poder elegir si la ejecución del mismo DoIndependentWork se ejecutará en modo Async o no

4) Se podrá definir la estrategia a seguir para hacer la invocación al siguiente Método

Cuando un método termine completamente su ejecución (Rol de DoIndependentWork), va a devolver a su invocante la cadena generada por éste mismo más lo generado por el siguiente método, como ilustra la figura.

![alt text](img/callStackMethods.png) 

1) El resultado final debe ser predicible,  para así validar si funciona correctamente la combinación de implementaciones/invocaciones.

2) Por otro lado, nos mostrará el tiempo tardado por cada método.

3) Se podrá visualizar el algoritmo que implementa dicha cascada.

4) Se visualizará el orden en que se crearon los puntos

La siguiente imagen es una muestra de lo que se pretende monitorizar con la aplicación.

![alt text](img/Example3Levels.png)

En la captura podemos ver como:

1) configuracion de la prueba en la parte superior izquierda

1.1) al elegir el número de niveles, se generan tantos niveles como el parámetro Levels es indicado ( en este caso 3)

1.2) se ha indicado que se muestre el nombre de la serie sobre la gráfica

3) los métodos generados en la parte inferior/central izquierda configura los métodos.

 3.1)  Tipo de implementación: Async o Sync 

 3.2) LOOPING de 25 o 65, indica que son iteraciones

 3.3) WRAPPER_ASYNC, indica que el Loopingse hace de manera asíncrono para los médodos 1 y 2. Para el método 3, se hace una iteración Normal.

 3.4)  WAIT_AFTER, es el modo que los Métodos 1 y 2, hacen su llamada asíncrona a sus respectivos siguientes. Más detalles, hacer click en los detalles de implementación.


4) La gráfica mostrada es por puntos. Se puede apreciar como los puntos del método 1 y 2 se intercalaron en la ejecución, mientras que los del método 3 no. Esto tiene su explicación.


# Para más detalles de ..

hacer click Aquí.. [Para saber más detalles de implementacion](HowToGraphicSeriesAreDone.md)

hacer click Aquí.. [Para saber más detalles de la aplicación](HowToWork.md)

hacer click Aquí.. [Conclusiones..](Conclusions.md)



