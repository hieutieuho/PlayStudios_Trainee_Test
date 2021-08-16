using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    private void Awake() {
        instance = this;
    }
    public GameObject Home, GamePlay, Setting;
    void ActiveHomeScene(bool active){
        Home.SetActive(active);
    }
    public void activeGameplay(bool active){
        ActiveHomeScene(!active);
        GamePlay.SetActive(active);
    }
    public void activeSetting(bool active){
        ActiveHomeScene(!active);
        Setting.SetActive(active);
    }
}
