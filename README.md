# IA Arboles de decisiones 
Juan Haro

## Parcial 1: *Mejora IA SPA*

### Planteamiento de la IA

Crear la IA para un NPC el cual ayude al jugador, ya sea eliminando enemigos, curando al jugador e incluso dándole munición cuando la necesite.

## Ejecucion 

En el archivo [SPA_Sheva.cs](Assets/_Parcial_1/Scripts/SPA_Sheva.cs) podemos encontrar la IA la cual realiza el proceso anterior. 

Esta IA cuenta con multiples ***sentidos*** los cuales son: 
* Percepción de la posición del jugador: En teoría al ser un aliado siempre debería de poder saber donde este se encuentra.
* Percepción de la vida del jugador: Misma lógica, es su aliado y puede saber si esta bien o necesita ayuda.
* Percepción de las balas del jugador: A este punto se entiende la idea.
* Percepción de sus propias estadísticas: Vida, balas, y cantidad de curaciones.
* Vista: Puede percibir enemigos en un cierto FOV, basado en un angulo de vision y en una distancia maxima.

En base a esto valora su situación y ***planea*** que acción puede ejecutar, siendo dichas acciones:
* Curar al jugador: En base a su porcentaje de vida y la cantidad de curas disponibles disponibles.
* Autocuración: Dependiendo de su porcentaje de vida y curas disponibles.
* Dar balas: Dará balas al jugador en base de cuantas le falten del máximo (50) y si el jugador tiene menos balas que la IA.
* Atacar: Si tiene algún enemigo a la vista, pero tomara prioridad de curar antes que de atacar, excepto si el jugador esta atacando a un enemigo que tenga a la vista.
* Vagar: Solo si no hay nada mas que hacer.

Una vez tomada la acción esta se ejecuta hasta que se complete o se tome alguna con mayor prioridad estas ***acciones*** constan de lo siguiente:
* Curar al jugador: Se acerca al jugador y gasta una de sus curas.
* Autocuración: Gasta una de sus curas y se cura.
* Dar balas: Se acerca al jugador y le de parte de su munición.
* Atacar: Busca un enemigo y lo ataca. Le da prioridad primero al ultimo enemigo que ataco, después al objetivo actual del jugador (ultimo objetivo que el jugador ataco) si no puede ver a ninguno de estos, atacara al enemigo mas cercano.
* Vagar: Se moverá alrededor del jugador y lo seguirá a donde vaya.