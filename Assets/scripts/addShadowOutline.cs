using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is the quick tool to add 4 shadow component in 4 directions to look like an outline
/// Why I don't use outline ? Because the out line cost twice the performance to 4 shadows !!! and the shadows look much better too!!!
/// </summary>


#if UNITY_EDITOR
[ExecuteInEditMode]
public class addShadowOutline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Shadow[] shads = gameObject.GetComponents<Shadow>();
        int shadsSize = shads.Length;
        if (shads.Length > 0)
            foreach (Shadow shad in shads)
                //StartCoroutine(DestroyComps(shad));
                DestroyImmediate(shad);
        //StartCoroutine(textBestfit());
        {
            Text txt = gameObject.GetComponent<Text>();
            if (txt != null)
            {
                txt.resizeTextForBestFit = true;
                txt.resizeTextMaxSize = txt.fontSize;
                txt.resizeTextMinSize = 10;
            }
        }
        if (shadsSize != 4)
        {
            for (int i = 0; i < 4; i++)
            {
                gameObject.AddComponent<Shadow>().effectColor = new Color(0.1254902f, 0.05490196f, 0);
            }
            Shadow[] shadows = gameObject.GetComponents<Shadow>();
            shadows[0].effectDistance = new Vector2(1, 1);
            shadows[1].effectDistance = new Vector2(-1, -1);
            shadows[2].effectDistance = new Vector2(1, -1);
            shadows[3].effectDistance = new Vector2(-1, 1);
        }
        //StartCoroutine(DestroySelf());
        DestroyImmediate(this);
    }
}

#endif
