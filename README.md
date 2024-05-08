# ENGLISH
Created by Jorge Torrent Silla.

## Introduction
Below, I present this project of a small 3D video game with the aim of showcasing code 100% implemented by myself. This code is fully owned by me, both its structural and functional parts. For any questions, you can contact me through the form located on [my website](https://jortosi.es).

## About the game
Using Unity as the engine and C# as the programming language, a 3D mini-game has been created where an ancient environment is simulated in a village where the protagonist is the village shopkeeper and the antagonist is a skeleton knight seeking revenge against you for removing the milk from your store, which caused him a bone disease. Trapped within the walls of the castle courtyard and using controller inputs, the user's objective is to jump on the enemy to reduce his health to 0 and avoid being hit.

## Resources Used
This section outlines the software theory resources needed for implementation as well as references to third-party resources.
### Architecture
The creation of 3 scenes has been chosen: the loading scene, the village scene, and the castle courtyard scene. Scene loading is done through a **Singleton** called *SceneLoader*, which has the public function *LoadScene(string sceneToLoad)*, where the name of the scene to load is stored publicly. Subsequently, the loading scene is synchronously loaded, where there is internal logic to asynchronously load the scene whose name was previously stored.

On the other hand, the village and castle courtyard scenes feature a prefab that is the universal level installer using the **ServiceLocator** pattern: it stores a list that holds references to all *GameObjects* that implement an interface called *ServicesConsumer*, and calls their *Install(ServiceLocator serviceLocator)* method. Each of these objects uses the service locator to store everything it needs.

Regarding the protagonist and antagonist, both use the same class architecture. Both are *Performers*. A *Performer* contains a series of *Actions*. Each *Action* performs a specific action, such as running, jumping, and attacking, and has an active or inactive state, which is stored in a *ScriptableObject* called *BoolProperty*. Each *Action*, in turn, stores a list of *BoolProperty* that prohibit its execution: if any of them are *true*, the action CANNOT be executed. This way, a system is achieved that ensures the 4th SOLID principle: the *Open-Close Principle*, which states that "a system should be open to extension but closed to modification". Thus, adding a new character is as simple as implementing a new *Performer*, and extending the behavior of a *Performer* is as simple as including a new *Action* and configuring its activation by expanding its list of prohibitive states.

On the other hand, there are collectibles, which inherit from the abstract class *Collectable*. This class holds the base logic, such as the oscillating movement and collision management with the protagonist. Child classes only need to implement 2 methods: *Collect()* and *Destroy()*. Their appearance is done through the **ObjectPool** pattern, which reuses collectibles to reduce memory usage.

### Third-party Libraries
All 3D models used in the project are from the *Low Poly Ultimate Pack* package. It is a low-poly 3D resource package created by *polyperfect*. It is available at the [following link](https://assetstore.unity.com/packages/3d/props/low-poly-ultimate-pack-54733) on the Unity Asset Store.

For the activation state of the *Actions*, reactive properties defined in the *UniRx* package by *neuecc* have been used. It is available at the [following link](https://github.com/neuecc/UniRx) on GitHub.

# ESPAÑOL
Creado por Jorge Torrent Silla.

## Introducción
A continuación, presento este proyecto de un pequeño videojuego en 3D con el objetivo de exponer código 100% implementado por mí mismo. Este código es de mi propiedad por completo, tanto su parte estructural como su parte funcional. Ante cualquier duda, se puede contactar conmigo a través del formulario ubicado en [mi página web](https://jortosi.es).

## Sobre el juego
Empleando Unity como motor y C# como lenguaje de programación, se ha creado un minijuego 3D donde se simula un entorno antiguo en una aldea donde el protagonista es el tendero del pueblo y el antagonista un caballero esqueleto que demanda venganza contra ti por el hecho de haber retirado la leche de tu tienda, lo que le provocó una enfermedad ósea. Encerrado entre las murallas del patio del castillo y haciendo uso de controles por mando, el objetivo del usuario será saltar sobre el enemigo para bajar su salud a 0 y evitar ser golpeado.

## Recursos empleados
En este apartado se exponen los recursos sobre teoría del software necesarios para la implementación así como las referencias a recursos de terceros.
### Arquitectura
Se ha optado por la creación de 3 escenas: la escena de loading, la escena de la aldea y la escena del patio del castillo. La carga de escenas se realiza a través de un **Singleton** llamado *SceneLoader*, que presenta la función pública *LoadScene(string sceneToLoad)*, en la cual se almacena el nombre de la escena a cargar de manera pública. Seguidamente, carga de manera síncrona la escena de loading, donde existe una lógica interna que hace cargar de manera asíncrona la escena cuyo nombre ha quedado almacenado previamente.

Por otra parte, las escenas de la aldea y del patio del castillo presentan un prefab que es el instalador universal del nivel haciendo uso del patrón **ServiceLocator**: almacena una lista que guarda las referencias a todos los *GameObject* que implementan una interfaz llamada *ServicesConsumer*, y llama a su método *Install(ServiceLocator serviceLocator)*. Cada uno de estos objetos hace uso del localizador de servicios para almacenar todo lo que le sea necesario.

En cuanto a protagonista y antagonista, ambos hacen uso de la misma arquitectura de clases. Ambos son *Performers*. Un *Performer* contiene una serie de *Actions*. Cada *Action* realiza una acción determinada, como correr, saltar y atacar, y presenta un estado de activa o no activa, el cual es almacenado en un *ScriptableObject* llamado *BoolProperty*. Cada *Action*, a su vez, guarda una lista de *BoolProperty* que le prohiben su ejecución: si alguna de ellas está a *true*, la acción NO podrá ser ejecutada. De esta manera, se consigue un sistema que asegura el 4º principio SOLID: el *Open-Close Principle*, el cual indica que "un sistema debe estar abierto a su extensión pero cerrado a su modificación". Así, añadir un nuevo personaje es tan sencillo como implementar un nuevo *Performer*, y extender el comportamiento de un *Performer* es tan sencillo como incluir una nueva *Action* y configurar su activación agrandando su lista de estados prohibitivos.

Por otra parte, están los coleccionables, los cuales heredan de la clase abstracta *Collectable*. Ésta guarda la lógica base, como el vaivén de movimiento y la gestión de la colisión con el protagonista. Las clases hijas únicamente deben implementar 2 métodos: *Collect()* y *Destroy()*. Su aparición se hace a través del patrón **ObjectPool**, mediante el cual se reutilizan los coleccionables para reducir el uso de memoria.

### Librerías de terceros
Todos los modelos 3D utilizados en el proyecto son del paquete *Low Poly Ultimate Pack*. Es un paquete de recursos 3D low poly creado por *polyperfect*. Está disponible en el [siguiente enlace](https://assetstore.unity.com/packages/3d/props/low-poly-ultimate-pack-54733) de la Unity Asset Store.

Para el estado de activación de las *Action*, se ha empleado el uso de propiedades reactivas definidas en el paquete *UniRx* de *neuecc*. Se encuentra disponible en el [siguiente enlace](https://github.com/neuecc/UniRx) de GitHub.
