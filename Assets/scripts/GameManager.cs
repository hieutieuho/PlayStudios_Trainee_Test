using System.Net.Mime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
public enum GameStatus
{
    Start,
    Finish
}

public class GameManager : MonoBehaviour
{
    // [Header("Drag the image of item to auto generate item properties")]
    // public Sprite itemsImg;

    [Header("List items for the game")]
    public List<M_Item> ListItems = new List<M_Item>();

    [Header("Number of how many items player can choose")]
    public int ChooseCount = 2;
    [Header("Number of how many items slot will show on scene")]
    public int NoS = 10; // Number of Slots

    [Header("Choose the image for the money")]
    public Sprite moneyIcon;

    [Space(10)]
    [Header("Sound Manager")]
    public List<ListAudioClip> listAudioClips;
    public AudioSource sound { get; private set; }
    public SoundManager soundManager;
    [HideInInspector]
    public GameStatus status = GameStatus.Start;
    public List<float> listProbability {get; private set;}
    public static GameManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        sound = FindObjectOfType<AudioSource>();
        soundManager = sound.GetComponent<SoundManager>();
        soundManager.PlayMusic();
        getListItem();
    }
    public void getListItem()
    {
        //get list items
        ListItems = JsonManager.getListItem();
        if (ListItems == null)
        {
            ListItems = getListItemFromImages();
            ListItemsJson listJson = new ListItemsJson();
            listJson.list = ListItems;
            JsonManager.saveJson(listJson, "Items");
        }
        // get list probabilities
        listProbability = new List<float>();
        listProbability = ProbabilityManager.instance.getListProbability(ListItems);
    }

    List<M_Item> getListItemFromImages()
    {
        List<M_Item> list = new List<M_Item>();
        List<Sprite> AllItemslist = Resources.LoadAll<Sprite>($"Items").ToList();
        for (int i = 0; i < AllItemslist.Count; i++)
        {
            M_Item item = new M_Item();
            item.id = AllItemslist[i].name;
            item.name = AllItemslist[i].name.First().ToString().ToUpper() + AllItemslist[i].name.Substring(1);
            if (AllItemslist[i].name.Contains("_"))
            {
                item.name = AllItemslist[i].name.Replace("_", " ");
            }
            item.weight = i + 1;
            item.message = $"You picked the {item.name} !!!";
            item.Probability = item.weight;
            list.Add(item);
        }
        return list;
    }
    public void onMuteSound(Text soundTxt){
        if(sound.mute == false){
            sound.mute = true;
        }else{
            sound.mute = false;
        }
        soundTxt.text = getSoundTxt();
    }
    public string getSoundTxt(){
        if(GameManager.instance.sound.mute == false){
            return $"{LeanLocalization.GetTranslationText("Sound")}: {LeanLocalization.GetTranslationText("On")}";
        }else{
            return $"{LeanLocalization.GetTranslationText("Sound")}: {LeanLocalization.GetTranslationText("Off")}";
        }
    }
    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif        
    }
}
