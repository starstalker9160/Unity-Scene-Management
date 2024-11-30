using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour {
    public static _SceneManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void switchScene(_SceneAsset trgt) {
        
    }
}