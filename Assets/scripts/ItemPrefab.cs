using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab : MonoBehaviour
{
    [SerializeField]
    Image Item_Image, Coin_Image;
    [SerializeField]
    Text Item_weight;
    public bool is_flip {get; private set;} = false;
    M_Item m_Item;    
    int[] Probability = {1,9,30,60};
    /// <summary>
    /// Initialize the items for the prefab
    /// </summary>
    /// <param name="m_Item"></param>
    public void init(M_Item m_Item){
        this.m_Item = m_Item;
        Item_Image.sprite = Resources.Load<Sprite>($"Items/{m_Item.id}");
        Item_weight.text = (m_Item.weight != 0) ? String.Format("{0:###,#}", m_Item.weight) : "0";
        Coin_Image.sprite = GameManager.instance.moneyIcon;
    }
    /// <summary>
    /// When player press on the items
    /// </summary>
    public void openItem(){
        #region random items

        int index = ProbabilityManager.getIndexByProbability(GameManager.instance.listProbability);
        init(GameManager.instance.ListItems[index]);

        #endregion
        
        #region open items
        if(GameManager.instance.status == GameStatus.Start){
            this.GetComponent<Animation>().Play("flip_Items");
            is_flip = true;
            ItemController.instance.checkOpenCount();
            SoundManager.PlaySound(Sound.Button);
        }
        #endregion
    }
    /// <summary>
    /// Trigger when the animation flip_items finish
    /// </summary>
    public void onEndOpenItemAnimation(){
        GameManager.instance.setActivePopup(true);
        PopUpControl.instance.setPopUpData(m_Item);
    }
    /// <summary>
    /// Trigger when player press Try Again button
    /// </summary>
    public void flipBackItem(){
        if(is_flip){
            is_flip = false;
            this.GetComponent<Animation>().Play("flip_back");
        }
    }
    /// <summary>
    /// Trigger when the animation flip_back finish
    /// </summary>
    public void onEndFlipBackAnimation(){
        ItemController.instance.onEndFlipBackAnimationCheck();
    }
}
