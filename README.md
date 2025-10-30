# Godot Utilities

> Modified based on ⬇️  
> - firebelly.GodotUtilities https://github.com/firebelley/GodotUtilities  
> - GodotSharp.SourceGenerators https://github.com/Cat-Lips/GodotSharp.SourceGenerators  

# Usage
Only built for Godot 4.  
Add `Rainfield.GodotUtilities` to your `.csproj`:  
https://www.nuget.org/packages/Rainfield.GodotUtilities.SourceGenerators/

# Source Generation
## [OnInstantiate]
Use this attribute to generate an `Instantiate` method that loads the `.tscn` file in the directory where the class file is located to instantiate and return the instance.  
The path is as follows:
```
folder/
    - MyScene.cs
    - MyScene.tscn
```

```csharp
using Godot;
using GodotUtilities;

public partial class MyScene : Node {
    [OnInstantiate]
    private void Init(string myArg1, int myArg2) {
        ...
    }
}
```

Generates(simplified):  
```csharp
    private static PackedScene __scene__;
    private static PackedScene __Scene__ => __scene__ ??= GD.Load<PackedScene>("res://path/to/MyScene.tscn");
    
    public static MyScene Instantiate(string myArg1, int myArg2) {
        var scene = __Scene__.Instantiate<MyScene>();
        scene.Init(myArg1, myArg2);
        return scene;
    }
```

Then you can use it like this:
```csharp
var scene = MyScene.Instantiate(myArg1, myArg2);
```

## [SceneWithInstantiate]
A simplified version of `OnInstantiate` that generates an `Instantiate` method without parameters.
```csharp
[SceneWithInstantiate]
public partial class MyScene : Node {
    ...
}
```
Then you can use it like this:
```csharp
var scene = MyScene.Instantiate();
```
## [Singleton]
Using this attribute to generate singleton code. The instance assigns itself to the static variable `Instance` of the class for ease of use, which will be set to null after the instance is freed.
```csharp
[Singleton]
public partial class MyScene : Node {
    ...
    
    public override partial void _EnterTree();
}
```

Generates(simplified):  
```csharp
    public static MyScene Instance { get; private set; }
    public override partial void _EnterTree() {
        Instance = this;
        TreeExiting += Clear;
    }
    private static void Clear() => Instance = null;
```
Then you can use it like this:
```csharp
var myScene = MyScene.Instance;
```
**Note that you need to add this line of code to ensure that it works properly**:  
```csharp
public override partial void _EnterTree();
```
The reason is as follows:    
https://github.com/godotengine/godot/issues/66597
