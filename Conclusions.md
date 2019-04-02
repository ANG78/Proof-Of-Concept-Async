# Casos.. ( Estamos en ello aún )

## Ejecucion Wait First, en 4 Métodos Async con los DoIndependentWork Asíncronos 

4 Niveles, 25 steps o 25 mls. El DoIndependentWork se ha ejecutado tb de manera ASÍNCRONA. 

Tras varias ejecuciones, los tiempos y la monitorización prácticamente son iguales.

Ha intervenido del orden de 3 hilos.

![Visión Global](img/636896649992199129_Point_M1_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M2_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M3_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M4_ASYNC_25LOOPINGWRAPPER_ASYNC.png)

Luego, se ha intentado incrementar el número de pasos del ultimo nivel, pero en dicho caso como era de esperar en las anteriores ejecuciones, no ha habido paralelización entre los niveles.

![Visión Global](img/636896655634221834_Point_M1_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M2_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M3_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_FIRST__M4_ASYNC_61LOOPINGWRAPPER_ASYNC.png)


## Ejecucion Wait First, en 4 Métodos Async con los DoIndependentWork No Asíncronos

Luego, se ha cambiado la ejecución de los DoIndependentWork de los 4 Métodos a modo No Asíncrono, para ver que ganamos o perdemos sobre el escenario de 4 metodos a 25 pasos Asíncronos, y la conclusión que tengo es que además de no penalizar en tiempo, reducimos la cantidad de hilos utilizados.

![Visión Global](img/636896664883130842_Point_M1_ASYNC_25LOOPINGNORMAL_WAIT_FIRST__M2_ASYNC_25LOOPINGNORMAL_WAIT_FIRST__M3_ASYNC_25LOOPINGNORMAL_WAIT_FIRST__M4_ASYNC_26LOOPINGNORMAL.png)



## Ejecucion Wait First, en 4 Métodos Async con sus DoIndependentWork Asíncronos y estrategia SLEEPING


Desde el punto de vista de la estrategia SLEEPING, al ser 250 mls cada nivel, el tiempo total de ejecución ha sido de 1 segundo más o menos.

Ha intervenido sólo un hilo.

![Visión Global](img/636896652448129600_Line_M1_ASYNC_25SLEEPINGWRAPPER_ASYNC_WAIT_FIRST__M2_ASYNC_25SLEEPINGWRAPPER_ASYNC_WAIT_FIRST__M3_ASYNC_25SLEEPINGWRAPPER_ASYNC_WAIT_FIRST__M4_ASYNC_25SLEEPINGWRAPPER_ASYNC.png)



## Ejecucion Wait After, en 4 Métodos Async con sus DoIndependentWork Asíncronos y estrategia LOOPING

4 Niveles, 25 steps o 25 mls. El DoIndependentWork se ha ejecutado tb de manera ASÍNCRONA. 

Tras varias ejecuciones, los tiempos son iguales pero la monitorización es cambiante entre ejecuciones.

Ha intervenido del orden de 5 hilos.

Se intercalan los 4 métodos en sus ejecuciones con la estrategia LOOPING.

![Visión Global](img/636896672785362823_Point_M1_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_25LOOPINGWRAPPER_ASYNC.png)

![Visión Global](img/636896672804373911_Point_M1_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_25LOOPINGWRAPPER_ASYNC.png)


Cuando se ha incrementado el número de pasos del metodo 4, se ha visto que llega un momento que los tres primeros niveles terminan su ejecución, y durante un tiempo se ejecuta sólamente el Método 4 hasta que termina.

![Visión Global](img/636896672912420091_Point_M1_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_25LOOPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_96LOOPINGWRAPPER_ASYNC.png)

## Ejecucion Wait After, en 4 Métodos Async con sus DoIndependentWork Asíncronos y estrategia SLEEPING

Ahora procedemos a pasar a la estrategia SLEEPING de los 4 métodos con un tiempo de 1000 mls cada uno y lanzado de manera ASINCRONA. Lo he lanzado 3 veces para ver si el resultado en cuanto a monitorización e hilos es el mismo.

![Visión Global](img/636896677148762396_Line_M1_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_100SLEEPINGWRAPPER_ASYNC.png)

![Visión Global](img/636896677129411289_Line_M1_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_100SLEEPINGWRAPPER_ASYNC.png)

![Visión Global](img/636896677084938745_Line_M1_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M2_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M3_ASYNC_100SLEEPINGWRAPPER_ASYNC_WAIT_AFTER__M4_ASYNC_100SLEEPINGWRAPPER_ASYNC.png)

En el se puede apreciar que el tiempo de ejecución es de prácticamente un segundo (se paralelizan los 4), que el resultado de la cadena es tb el esperado y que además se ejecuta todo en un mismo hilo.

## Ejecucion Wait After, en 4 Métodos Async con sus DoIndependentWork Síncronos y estrategia SLEEPING

Ahora, vamos a hacer la prueba con esta misma estrategia Sleeping, pero los DoIndependentWork lanzandose de manera no Asincrona (NORMAL).

![Visión Global](img/636896684207336123_Line_M1_ASYNC_100SLEEPINGNORMAL_WAIT_AFTER__M2_ASYNC_100SLEEPINGNORMAL_WAIT_AFTER__M3_ASYNC_100SLEEPINGNORMAL_WAIT_AFTER__M4_ASYNC_100SLEEPINGNORMAL.png)

Como se puede apreciar, ahora es el mismo hilo el que atiende a las 4 métodos pero se tarda 4 segundos en las ejecuciones. Además, tras varias ejecuciones la monitorización parece ser más estable.


