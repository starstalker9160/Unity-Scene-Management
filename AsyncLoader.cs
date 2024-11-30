using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour {
    private Slider loadingSlider;
    private bool m_loading = false;

    public void LoadLvl(_SceneAsset lvl) {
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
    }
}
