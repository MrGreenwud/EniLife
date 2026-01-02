# EniLife
Lightweight lifetime-based resource and subscription management for Unity (Runtime and Editor).

This library provides a simple and explicit way to control object lifetime, resource disposal, and event subscriptions using a single concept — Lifetime.

## Core Concept
```Lifetime``` represents a period of time during which a resource, subscription, or logic is valid.

### Basic Usage
```cs
var handle = new LifetimeHandle();
Lifetime lifetime = handle.Lifetime;

var texture = new RenderTexture(256, 256, 0)
    .AutoDispose(lifetime);

// ...

handle.Terminate(); // texture is disposed automatically
```

### Nested Lifetimes
```cs
var root = new LifetimeHandle();
var child = root.CreateNested();

child.Lifetime.OnTerminate(() =>
{
    Debug.Log("Child lifetime terminated");
});

root.Terminate(); // terminates child as well
```
Nested lifetimes are useful for scoped logic such as UI screens, temporary systems, or async operations.

### RuntimeLifetime
Global lifetime available during application runtime.
```cs
var gameObject = new GameObject("Example")
    .AutoDispose(RuntimeLifetime.Global);
```
Behavior:
- created automatically on startup
- survives scene loading
- terminates on application shutdown

### EditorLifetime
Global lifetime for Unity Editor usage.
```cs
var texture = new RenderTexture(128, 128, 0)
    .EditorAutoDispose();
```
Behavior:
- terminates before assembly reload
- prevents resource leaks between recompilations

### CancellationToken
Convert a lifetime to ```CancellationToken```:
```cs
var token = lifetime.ToCancellationToken();
```
The token is cancelled when the lifetime terminates.

## AutoDispose
### Unity Resources
```cs
renderTexture.AutoDispose(lifetime);
unityObject.AutoDispose(lifetime);
gameObject.AutoDispose(lifetime);
```
### Custom Dispose Logic
```cs
resource.AutoDispose(lifetime, r => r.Release());
```
### IDisposable
```cs
lifetime.AutoDispose(disposable);
```

## Signal
```Signal``` is a lightweight event system bound to lifetimes.
```cs
var signalHandle = new SignalHandle();
var signal = signalHandle.Signal;

signal.Subscribe(lifetime, () =>
{
    Debug.Log("Signal fired");
});

signalHandle.Fire();
```

## Thread Safety
- ```LifetimeHandle``` is thread-safe and can be terminated from any thread
- cleanup actions are executed exactly once
- ```Signal``` is intended for main-thread usage
