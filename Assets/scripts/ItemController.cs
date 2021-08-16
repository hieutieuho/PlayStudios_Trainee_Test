using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    GridLayoutGroup gridLayout;
    [SerializeField]
    GameObject ItemPrefab, tryAgainBtn;
    [SerializeField]
    Text soundTxt;
    int open_count = 0;
    int flip_back_count = 0;
    public static ItemController instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        loadItems();
    }
    private void OnEnable()
    {
        soundTxt.text = GameManager.instance.getSoundTxt();
        for (int i = 0; i < gridLayout.transform.childCount; i++)
        {
            int c = i;
            StartCoroutine(DelayAction(i * 0.1f, () =>
            {
                gridLayout.transform.GetChild(c).GetComponent<ItemPrefab>().playShowUpAnimation();
            }));
        }
    }
    private void OnDisable()
    {
    }

    /// <summary>
    /// Load items data 
    /// </summary>
    void loadItems()
    {
        AddItemGameObject();
        //setItemData();
    }
    /// <summary>
    /// wait for amount of time before doing something
    /// </summary>
    /// <param name="time"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    IEnumerator DelayAction(float time, Action callback)
    {
        yield return new WaitForSecondsRealtime(time);
        callback?.Invoke();
    }
    void AddItemGameObject()
    {
        for (int i = 0; i < GameManager.instance.NoS; i++)
        {
            GameObject obj = Instantiate(ItemPrefab, gridLayout.transform);
            StartCoroutine(DelayAction(i * 0.1f, () => obj.GetComponent<ItemPrefab>().playShowUpAnimation()));
        }
    }
    // void setItemData()
    // {
    //     List<M_Item> items = GameManager.instance.ListItems;
    //     List<M_Item> listRand = new List<M_Item>();
    //     for (int i = 0; i < items.Count; i++)
    //     {
    //         M_Item randItem = items[UnityEngine.Random.Range(0, items.Count)];
    //         while (listRand.Any(x => x.id == randItem.id))
    //         {
    //             randItem = items[UnityEngine.Random.Range(0, items.Count)];
    //         }
    //         listRand.Add(randItem);
    //     }
    //     for (int i = 0; i < listRand.Count; i++)
    //     {
    //         gridLayout.transform.GetChild(i).GetComponent<ItemPrefab>().init(listRand[i]);
    //     }
    // }

    /// <summary>
    /// Check how many items player opened
    /// </summary>
    public void checkOpenCount()
    {
        open_count++;
        if (open_count == GameManager.instance.ChooseCount)
        {
            tryAgainBtn.SetActive(true);
            GameManager.instance.status = GameStatus.Finish;
            GameManager.instance.setGuideTxt();
        }
    }
    public void checkCountForSound()
    {
        if (open_count == GameManager.instance.ChooseCount)
        {
            GameManager.instance.soundManager.PlaySound(Sound.endGame);
        }
        else
        {
            GameManager.instance.soundManager.PlayMusic();
        }
    }
    /// <summary>
    /// event when press Try Again button
    /// </summary>
    public void onTryAgainBtn()
    {
        foreach (Transform item in gridLayout.transform)
        {
            item.GetComponent<ItemPrefab>().flipBackItem();
        }
    }
    /// <summary>
    /// Trigger when the animation flip_back finish
    /// </summary>
    public void onEndFlipBackAnimationCheck()
    {
        flip_back_count++;
        if (flip_back_count == GameManager.instance.ChooseCount)
        {
            resetItems();
        }
    }
    public void onPressHomeBtn(){
        
        for (int i = 0; i < gridLayout.transform.childCount; i++)
        {
            gridLayout.transform.GetChild(i).GetComponent<ItemPrefab>().changeAnimatorState(ItemState.IDLE);
        }
        resetItems();
        SceneManager.instance.activeGameplay(false);
    }
    public void onPressSoundBtn(){
        GameManager.instance.onMuteSound(soundTxt);
    }
    /// <summary>
    /// Reset game variables and reArrange items postion
    /// </summary>
    void resetItems()
    {
        flip_back_count = 0;
        open_count = 0;
        GameManager.instance.status = GameStatus.Start;
        GameManager.instance.setGuideTxt();
        tryAgainBtn.SetActive(false);
        GameManager.instance.soundManager.PlayMusic();
    }
}
