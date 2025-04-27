using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scene Management/Scene Asset")]
public class _SceneAsset : ScriptableObject {
    [SerializeField] private SceneAsset Scene;
    [HideInInspector] public string sceneName;
    public int priority = 1;
    public int weight = 5;
    public ShaderVariantCollection SVC;
    public HashSet<_SceneAsset> neighbors = new HashSet<_SceneAsset>();

    private void OnValidate() { sceneName = Scene.name; }
}