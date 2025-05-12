using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour {
    public int memoryBuffer = 6;
    public _SceneAsset currentScene;
    public HashSet<_SceneAsset> loadedScenes = new HashSet<_SceneAsset>();

    public static _SceneManager Instance {
        get {
            if (_instance == null) {
                GameObject loaderObject = new GameObject("_SceneManager");
                _instance = loaderObject.AddComponent<_SceneManager>();
                DontDestroyOnLoad(loaderObject);
            }
            return _instance;
        }
    }

    private static _SceneManager _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) { Destroy(gameObject); }
    }

    /// <summary>
    /// Load scene in background
    /// </summary>
    /// <param name="scene">The target scene</param>
    public IEnumerator loadInBG(_SceneAsset trgt) {
        if (currentScene == trgt || loadedScenes.Contains(trgt)) { yield break; }
        AsyncOperation loadOpp = SceneManager.LoadSceneAsync(trgt.sceneName, LoadSceneMode.Additive);
        while (!loadOpp.isDone) { yield return null; }
        loadedScenes.Add(trgt);
        Debug.Log("[  OK  ] Scene loaded in background");
    }

    /// <summary>
    /// Switch to a scene, regardless if or not its loaded
    /// </summary>
    /// <param name="scene">The target scene</param>
    private bool switching = false;
    public void switchScene(_SceneAsset trgt) {
        if (currentScene == trgt || switching) { return; }
        switching = true;
        if (loadedScenes.Contains(trgt)) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(trgt.sceneName));
            return;
        }
        _SceneLoader.Instance.LoadLvl(trgt);
        loadedScenes.Add(trgt);
        switching = false;
    }

    /// <summary>
    /// Checks if a scene is currently loaded
    /// </summary>
    /// <param name="scene">The scene to check for being loaded</param>
    /// <returns>True if the scene is loaded, otherwise false</returns>
    public bool isSceneLoaded(_SceneAsset scene) {
        return loadedScenes.Contains(scene);
    }
}