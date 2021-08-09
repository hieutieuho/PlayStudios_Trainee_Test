using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class JsonManager
{
    #region get & save json file
    static string readJson(string name)
    {
        string json = "";
        TextAsset text = Resources.Load<TextAsset>($"Json/{name}");
        if (text == null)
        {
            Debug.LogWarning("JSON NOT EXIT");
            return null;
        }
        json = text.ToString();
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("JSON EMPTY");
            return null;
        }
        return json;
    }
    public static void saveJson<T>(T value, string name)
    {
        string dataAsJson = JsonUtility.ToJson(value, true);
        string filePath = $"{Application.dataPath}/Resources/Json/{name}.json";
        File.WriteAllText(filePath, dataAsJson);
    }
    #endregion
    
    #region get json files by names
    public static List<M_Item> getListItem(){
        ListItemsJson list = new ListItemsJson();
        string json = readJson("Items");
        list = JsonUtility.FromJson<ListItemsJson>(json);
        if(list != null)
            return list.list;
        else
            return null;
    }
    #endregion
}
[Serializable]
public class ListItemsJson
{
    public List<M_Item> list = new List<M_Item>();
}