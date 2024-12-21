using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class _SceneLoader : MonoBehaviour {

    private Slider loadingSlider;
    private bool m_loading = false;

    public static _SceneLoader Instance {
        get {
            if (_instance == null) {
                GameObject loaderObject = new GameObject("_SceneLoader");
                _instance = loaderObject.AddComponent<_SceneLoader>();
                DontDestroyOnLoad(loaderObject);
            }
            return _instance;
        }
    }

    private static _SceneLoader _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) { Destroy(gameObject); }
    }

    public void LoadLvl(_SceneAsset lvl) {
        // ree
        Debug.Assert(!m_loading, "[ FAIL ] Loading already in progress");
        if (m_loading) { return; }
        StartCoroutine(loadLoadingScene(lvl));
    }

    private IEnumerator loadLoadingScene(_SceneAsset trgt) {
        m_loading = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        while (!operation.isDone) { yield return null; }

        GameObject sliderObject = GameObject.FindGameObjectWithTag("loadingBar");
        if (sliderObject != null) { loadingSlider = sliderObject.GetComponent<Slider>(); }
        loadingSlider.value = 0;
        Debug.Log("[  OK  ] Started loading...");
        StartCoroutine(loadTargetScene(trgt));
    }

    private IEnumerator loadTargetScene(_SceneAsset trgt) {
        Debug.Log("[  OK  ] Compiling shaders");
        ShaderVariantCollection svc = trgt.SVC;
        if (svc != null && svc.variantCount != 0) { svc.WarmUp(); }
        Debug.Log("[  OK  ] Done compiling shaders");

        AsyncOperation loadOpp = SceneManager.LoadSceneAsync(trgt.sceneName);
        while (!loadOpp.isDone) {
            float progressValue = Mathf.Clamp01(loadOpp.progress / 0.9f);
            if (loadingSlider != null) { loadingSlider.value = progressValue; }
            yield return null;
        }
        m_loading = false;
    }
    
}