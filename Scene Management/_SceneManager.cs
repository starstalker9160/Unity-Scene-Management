using System;
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

    public IEnumerator loadInBG(_SceneAsset trgt) {
        if (currentScene == trgt || loadedScenes.Contains(trgt)) { yield break; }
        AsyncOperation loadOpp = SceneManager.LoadSceneAsync(trgt.sceneName, LoadSceneMode.Additive);
        while (!loadOpp.isDone) { yield return null; }
        loadedScenes.Add(trgt);
        Console.WriteLine("[  OK  ] Scene loaded in background");
    }

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

    public bool isSceneLoaded(_SceneAsset scene) {
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name == scene.sceneName) { return true; }
        }
        return false;
    }
}