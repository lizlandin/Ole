# ¡OLÉ! 
## Vertical Slice

**¡OLÉ!** es un videojuego 3D creado en Unity en el que el jugador debe sobrevivir dentro de una plaza de toros y recoger todas las estrellas mientras es perseguido constantemente por un toro.

## Objetivo

Para ganar la partida, el jugador debe:

- Recolectar las 5 estrellas que están en la arena.
- Sobrevivir durante 1 minuto y 30 segundos.
- Evitar los ataques del toro y utilizar los obstáculos de la arena para escapar.
- Haber recolectado todas las estrellas y tener por lo menos una vida cuando acabe el tiempo.

Si el jugador pierde sus 3 vidas o no consigue todas las estrellas antes de que termine el tiempo, pierde la partida.

## Controles

| Acción | Control |
|---|---|
| Moverse | W, A, S, D / Flechas |
| Saltar | Barra espaciadora |

## Mecánicas principales

### Persecución del toro
El toro persigue constantemente al jugador dentro de la arena. Al entrar en contacto con él, el jugador pierde una vida y recibe un pequeño periodo de invulnerabilidad para que no pueda perder vidas tan rápido y seguido si queda mucho tiempo en contacto con el toro.

Cuando quedan solamente 20 segundos, el toro se enfurece y aumenta su velocidad.

### Recolección de estrellas
Existen 5 estrellas distribuidas por la arena. Algunas requieren utilizar los obstáculos y el salto para poder alcanzarlas.

El jugador debe recolectar todas antes de que termine el tiempo.

### Sistema de vidas
El jugador comienza cada partida con 3 vidas y cada golpe del toro reduce una vida.

Al llegar a 0 vidas se muestra la pantalla de derrota y la partida se reinicia.

### Tiempo
Cada partida dura 1 minuto y 30 segundos.

Antes de comenzar se muestra una cuenta regresiva:

**3 → 2 → 1 → ¡OLÉ!**

Al terminar el tiempo, el juego checa si el jugador recolectó todas las estrellas para saber si ganó o perdió.

## Victoria y derrota

Al ganar, el jugador realiza una animación de celebración y aparece una pantalla de victoria desde donde se puede reiniciar o salir del juego.

Al perder, aparece una pantalla de derrota y la partida se reinicia automáticamente después de unos segundos.

## Elementos del juego

El proyecto incluye:

- Modelos y escenario 3D.
- Animaciones para el jugador y el toro.
- Sistema de partículas.
- Iluminación nocturna.
- Obstáculos con físicas.
- Interfaz gráfica personalizada.
- Música de fondo.
- Sonido ambiental del público.
- Sistema de victoria y derrota.
- Menú de inicio y cuenta regresiva.

## Desarrollo

El videojuego fue desarrollado utilizando:

- **Unity**
- **C#**
- **TextMeshPro**
- **Post Processing** ( Para arreglar un asset que no se veía como debería )
- **Git y GitHub** para el control de versiones.

## Organización

Los principales elementos del proyecto se encuentran organizados dentro de `Assets` en carpetas para:

- Modelos
- Scripts
- Animaciones
- Prefabs
- UI
- Audio
- Partículas
- Materiales

## Uso de Inteligencia Artificial

Durante el desarrollo del vertical slice se utilizó Inteligencia Artificial como una herramienta de apoyo para resolver algunos problemas específicos de funcionamiento o para mejorar aspectos que todavía no sabía cómo implementar.

Las funciones en las cuales fue usada la Inteligencia Artificial vienen marcadas con comentarios explicando en qué me ayudó y el porqué.

Todas las soluciones sugeridas fueron revisadas y adaptadas de acuerdo con las necesidades del proyecto.

