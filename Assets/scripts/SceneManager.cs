using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    const string
        MainScene = "MainScene",
        Gameplay = "Gameplay";
    public static SceneManager instance;
    private void Awake() {
        instance = this;
    }
    public GameObject Home, Setting;
    void ActiveHomeScene(bool active){
        Home.SetActive(active);
    }
    public void GoToGamePlay(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(Gameplay);
    }
    public void GotoHome(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainScene);
    }
    public void activeSetting(bool active){
        ActiveHomeScene(!active);
        Setting.SetActive(active);
    }
}
