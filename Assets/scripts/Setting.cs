using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class Setting : MonoBehaviour
{
    [SerializeField]
    Text soundTxt, languageTxt;
    [SerializeField]
    NativeAndroidTextSharingInUnity share;
    LeanLocalization leanLocalization;
    private void OnEnable()
    {
        leanLocalization = FindObjectOfType<LeanLocalization>();
        soundTxt.text = GameManager.instance.getSoundTxt();
    }
    public void onPressSoundBtn()
    {
        GameManager.instance.onMuteSound(soundTxt);
    }
    public void onPressLanguage()
    {
        if (leanLocalization.CurrentLanguage == "EN")
        {
            LeanLocalization.SetCurrentLanguageAll("VN");
        }
        else
        {
            LeanLocalization.SetCurrentLanguageAll("EN");
        }
        setText();
    }
    void setText()
    {
        soundTxt.text = GameManager.instance.getSoundTxt();
        GameManager.instance.setGuideTxt();
        if (leanLocalization.CurrentLanguage == "EN")
        {
            languageTxt.text = $"{LeanLocalization.GetTranslationText("Language")}: {leanLocalization.CurrentLanguage}";
        }
        else
        {
            languageTxt.text = $"{LeanLocalization.GetTranslationText("Language")}: {leanLocalization.CurrentLanguage}";
        }
    }
    public void onSupportBtn()
    {
        share.ShareText();        
    }
}
