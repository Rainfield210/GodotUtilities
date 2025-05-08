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
Use this attribute on a method to generate an `Instantiate` static method that loads the `.tscn` file in the directory where the class file is located, instantiates it, and calls the attributed method with the provided parameters.  

**Note**: You need to add the following property to your `.csproj` file to tell the source generator the path to the Godot project file:
```xml
<GodotProjectDir>$([MSBuild]::GetDirectoryNameOfFileAbove($(MSBuildProjectDirectory), 'project.godot'))</GodotProjectDir>
```

The path structure should be:
```
folder/
    - MyScene.cs
    - MyScene.tscn
```

```csharp
using Godot;
using GodotUtilities.SourceGenerators.OnInstantiateExtensions;

public partial class MyScene : Node {
    [OnInstantiate]
    private void Init(string myArg1, int myArg2) {
        ...
    }
}
```

Generates (from OnInstantiateTemplate.sbncs):  
```csharp
using Godot;

namespace YourNamespace;

partial class MyScene {
    private static PackedScene __Scene__ => field ??= GD.Load<PackedScene>("res://path/to/MyScene.tscn");

    public static MyScene Instantiate(string myArg1, int myArg2) {
        var scene = __Scene__.Instantiate<MyScene>();
        scene.Init(myArg1, myArg2);
        return scene;
    }
}
```

Then you can use it like this:
```csharp
var scene = MyScene.Instantiate(myArg1, myArg2);
```


## [SceneWithInstantiate]
A simplified version of `OnInstantiate` that generates an `Instantiate` static method without parameters. Use this attribute on a class.
```csharp
using GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

[SceneWithInstantiate]
public partial class MyScene : Node {
    ...
}
```

Generates (from SceneWithInstantiateTemplate.sbncs):
```csharp
using Godot;

namespace YourNamespace;

partial class MyScene {
    private static PackedScene __Scene__ => field ??= GD.Load<PackedScene>("res://path/to/MyScene.tscn");

    public static MyScene Instantiate() => __Scene__.Instantiate<MyScene>();
}
```

Then you can use it like this:
```csharp
var scene = MyScene.Instantiate();
```
## [Singleton]
Use this attribute on a class to generate singleton code. The instance assigns itself to the static variable `Instance` when entering the tree and clears it when exiting the tree.
```csharp
using GodotUtilities.SourceGenerators.SingletonExtensions;

[Singleton]
public partial class MyScene : Node {
    ...
    
    public override partial void _EnterTree();
}
```

Generates (from SingletonTemplate.sbncs):  
```csharp
namespace YourNamespace;

partial class MyScene {
    public static MyScene Instance { get; private set; }

    public override partial void _EnterTree() {
        Instance = this;
        TreeExiting += ClearInstance;
    }

    private void ClearInstance() {
        if(Instance == this) { 
            Instance = null;
    	}
    	TreeExiting -= ClearInstance;
    }
}
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
