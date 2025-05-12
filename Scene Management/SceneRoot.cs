using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour {
    [SerializeField] private GameObject wrapper;
    [SerializeField] private _SceneAsset HUD;

    private void Awake() {
        if (!_SceneManager.Instance.isSceneLoaded(HUD)) { StartCoroutine(thingy()); }
        else { wrapper.SetActive(true); }
    }

    private IEnumerator thingy() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) { yield return null; }
        wrapper.SetActive(true); 
    }
}