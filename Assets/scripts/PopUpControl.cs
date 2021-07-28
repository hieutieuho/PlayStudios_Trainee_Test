using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpControl : MonoBehaviour
{
    [SerializeField]
    Image itemImg, coinImage;
    [SerializeField]
    Text itemMessage;
    [SerializeField]
    Text itemWeight;
    [SerializeField]
    ParticleSystem particle;
    public static PopUpControl instance;
    void Awake()
    {
        instance = this;
    }
    public void setPopUpData(M_Item m_Item)
    {
        if(m_Item.is_jackpot){
            particle.Play();
        }
        itemImg.sprite = Resources.Load<Sprite>($"Items/{m_Item.id}");
        itemMessage.text = m_Item.message;
        coinImage.sprite = GameManager.instance.moneyIcon;
        increasingNumberEffect(0, m_Item.weight, 1);
        SoundManager.PlaySoundItem(m_Item.weight);
    }
    /// <summary>
    /// Increase the text number in an amount of time
    /// </summary>
    /// <param name="fromValue"> From value</param>
    /// <param name="toValue"> To Value </param>
    /// <param name="duration"> Duration </param>
    /// <param name="tk"></param>
    /// <returns></returns>
    async void increasingNumberEffect(int fromValue, int toValue, float duration, CancellationToken tk = default(CancellationToken))
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            if (tk.IsCancellationRequested) break;
            float progress = timer / duration;
            try
            {
                itemWeight.text = ((toValue != 0) ? ((toValue > 0) ? "+" + String.Format("{0:###,#}", ((int)Mathf.Lerp(fromValue, toValue, progress))) : String.Format("{0:###,#}", ((int)Mathf.Lerp(fromValue, toValue, progress)))) : "+0");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            await Task.Yield();
        }
        try
        {
            itemWeight.text = ((toValue != 0) ? ((toValue > 0) ? "+" + String.Format("{0:###,#}", toValue) : String.Format("{0:###,#}", toValue)) : "+0");
        }
        catch (Exception e)
        {

            Debug.LogError(e.Message);
        }
    }
    public void onClose(){
        ItemController.instance.checkCountForSound();
    }
}
