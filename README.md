# Unity Scene Management

## Installation

1. In your Unity project's `Scripts` folder, create a new folder called `Editor`
2. Drop the provided files into the `Editor` folder

Your file structure should look like this:
<pre> ``` Assets/
	└── Scripts/
		└── Editor/
			├── Scene Management
			└── Project.Editor.asmdef ``` </pre>

## Usage

example.cs :
```csharp
using UnityEditor;
using UnityEngine;

public class ExampleUsage {
	public _SceneAsset scene;
	public void exampleFunctionToLoadASceneInBackground() {
		_SceneManager.Instance.loadInBG(scene);
	}
	public void exampleFunctionToSwitchToAScene() {
		_SceneManager.Instance.switchScene(scene);
	}
	public void exampleFunctionToCheckIfASceneHasAlreadyBeenLoaded() {
		Debug.Log(_SceneManager.Instance.isSceneLoaded(scene) ? "scene is already loaded" : "scene is not yet loaded");
	}
}
```
