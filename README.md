# Almost Good Engine

## Installation
The Almost Good Engine is segmented in multiple independent parts.  
You don't have to install the whole engine if you are only interested by the Input system.

However, if you want to use the entire engine, you can install visual studio templates to get a quick start, or install AlmostGoodStudio.Core to get the complete version of the engine.

## Features

### AlmostGoodEngine.AI
This package includes multiple artificial intelligence algorithms  
- [X] A* pathfinding  
- [ ] Nav mesh  
- [ ] Nav agent  

### AlmostGoodEngine.Inputs
This package comes with classes which will helps you manage inputs in your game.  
- [X] Keyboard listener  
- [X] Mouse listener  
- [X] Gamepad listener  
- [X] Touch screen listener  
- [X] Input binding  

### AlmostGoodEngine.Audio
You are tired of the Song issues? Our Audio system will help you implements your sounds and musics and help you to loop or pitch your sounds without the issues of the Song class.  
- [X] Audio channels  
- [X] Audio listener  
- [ ] Max sound instances  
- [ ] Audio effects
- [ ] Spacial sounds

### AlmostGoodEngine.Animations
The animations packages is your best friend to animate everything you want.
It contains a Tweener, a Animation state engine and a Coroutine system.  
- [X] Keyframes animations  
- [X] Spritesheet animation  
- [X] Coroutine engine  
- [ ] Tweens animations  
- [ ] Bones animations  

### AlmostGoodEngine.GUI
Do you want a fresh alternative of ImGUI? Our GUI library allow you to create complex interfaces using a simple API.  
- [X] GUI core element  
- [X] CSS loading styles  
- [ ] Predefined elements (menu, buttons, user inputs, ...)

### AlmostGoodEngine.Physics
Don't waste time to reimplement a physics engine. Our engine will cover all the usage you need.  
- [ ] Collision detection
- [ ] Rigidbody

### AlmostGoodEngine.Core
A great place to start with, the core package contains all the Almost Good Engine packages and will help you create your game using an intuitive ECS architecture and a lot of components to start with.  
- [X] ECS  
- [X] Camera rendering  
- [X] 2D atlases  
- [X] Sprite 2D  
- [X] Path 2D  
- [X] Animated sprite 2D  
- [X] Timer  
- [X] Scene manager  
- [ ] In-engine console

### AlmostGoodEngine.Serialization
The serialization package will give you all the keys to help you save and load files in your game
- [X] Json serialization/deserialization
- [X] XML serialization/deserialization

### AlmostGoodEngine.Editor
The editor bring you and easy to learn interface helping you using the engine features.
- [ ] User Interface
- [ ] Component inspector
- [ ] Custom properties
- [ ] Tileset editor
- [ ] Animation editor
- [ ] Sprite animation editor
- [ ] Map editor
- [ ] Prefab editor