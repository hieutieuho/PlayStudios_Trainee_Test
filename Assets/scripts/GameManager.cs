using System.Net.Mime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStatus
{
    Start,
    Finish
}

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    [Header("Drag the image of item to auto generate item properties")]
    public Sprite itemsImg;
    [Header("List items for the game")]
    public List<M_Item> ListItems = new List<M_Item>();
    [Header("Number of how many items player can choose")]
    public int ChooseCount = 2;
    [Header("Choose the image for the money")]
    public Sprite moneyIcon;
    [Space(10)]
    [Header("Reference components")]
    [SerializeField]
    GameObject Popup;
    [SerializeField]
    Text guideTxt;
    [Space(10)]
    [Header("Sound Manager")]
    public List<ListAudioClip> listAudioClips;
    public AudioSource sound {get; private set;}
    [HideInInspector]
    public GameStatus status = GameStatus.Start;
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        setGuideTxt();
        sound = FindObjectOfType<AudioSource>();
        SoundManager.PlayMusic();
    }
    public void setGuideTxt(){
        if(status == GameStatus.Start){
            guideTxt.text = $"Pick {ChooseCount} slot to test your luck !!!";
        }else{
            guideTxt.text = $"Good luck next time, Try Again ======>>>";
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        bool check = false;
        foreach (var item in ListItems)
        {
            if (Resources.Load<Sprite>($"Items/{item.id}") == null)
            {
                Debug.LogError($"This item {item.id} is missing !!!");
                check = true;
                break;
            }
        }
        if (!check)
        {
            StartCoroutine(ClearConsole());
        }
        if (itemsImg != null)
        {
            if (!ListItems.Any(x => x.id == itemsImg.name))
            {
                M_Item item = new M_Item();
                item.id = itemsImg.name;
                item.name = itemsImg.name.First().ToString().ToUpper() + itemsImg.name.Substring(1);
                if (item.name.Contains("_"))
                    item.name = item.name.Replace("_", " ");
                item.weight = ListItems.Count + 1;
                item.message = $"You picked the {item.name} !!!";
                ListItems.Add(item);
            }
            else
            {
                Debug.LogError("This item already exist !!!");
            }
        }
        itemsImg = null;
    }
    IEnumerator ClearConsole()
    {
        // wait until console visible
        while (!Debug.developerConsoleVisible)
        {
            yield return null;
        }
        yield return null; // this is required to wait for an additional frame, without this clearing doesn't work (at least for me)
        Debug.ClearDeveloperConsole();
    }
#endif
    public void setActivePopup(bool set)
    {
        Popup.SetActive(set);
    }
}
