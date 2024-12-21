using UnityEngine;

public class MainMenu : MonoBehaviour {
    public void LoadLvl(_SceneAsset lvl) { _SceneLoader.Instance.LoadLvl(lvl); }
    public void quit() { Application.Quit(); }
}
