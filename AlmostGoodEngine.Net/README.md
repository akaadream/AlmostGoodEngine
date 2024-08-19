# Almost Good Engine - Net

The net package of the engine. This is a good start if you want to implement an multiplayer mode inside your game.

> The package is currently limited to a Peer to peer net code

## Installation

This package is part of the AlmostGoodEngine.Core package meaning that if you already got the core package, this one is included.
If you want to use this package standalone, you can install it via the NuGeT package manager or using the following command:

```
>
```

## Usage

The host of the server need to use the `Server attribute`:

```csharp
[Server(id, port)]
```

Then, each client will need to use the `Client attribute`:
```csharp
[Client(id, address, port)]
```

Now, let's say you got a player which the host and another player which is the client.
Both of them got a `Vector2 position { get; set; }`.  
If you want the position synchronized between the server and the client, you can just use the `Sync attribute`:
```csharp
[Sync]
public Vector2 Position { get; set; }
```

And voila, the position between the players is synchronized!