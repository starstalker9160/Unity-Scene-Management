using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scene Management/Scene Asset")]
public class _SceneAsset : ScriptableObject {
    public string sceneName;
    public int priority = 1;
    public int weight = 5;
    public ShaderVariantCollection SVC;
    public List<_SceneAsset> neighbors = new List<_SceneAsset>();
}