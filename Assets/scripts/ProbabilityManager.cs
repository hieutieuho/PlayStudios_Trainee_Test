using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityManager
{
    public static List<float> getListProbability(List<M_Item> listItem){
        return listItem.Select(x => x.Probability).ToList<float>();
    }
    static List<float> makeProbabilityRange(List<float> listProbability){
        float ProbabilitySum = 0;
        List<float> listProbabilityRange = new List<float>();
        foreach(float f in listProbability){
            ProbabilitySum += f;
            listProbabilityRange.Add(ProbabilitySum);
        }
        if(ProbabilitySum > 100){            
            Debug.LogError("Probability sum exceed 100%");
            return null;
        }
        return listProbabilityRange;
    }
    public static int getIndexByProbability(List<float> listProbability){
        List<float> probabilityRange = makeProbabilityRange(listProbability);

        float rand = Random.Range(1f,101f);
        Debug.Log($"Rand number : {rand}");
        
        for(int i = 0; i < listProbability.Count; i++){
            if(rand <= probabilityRange[i]){
                return i;
            }
        }
        return -1; //return -1 if some error happen
    }
}
