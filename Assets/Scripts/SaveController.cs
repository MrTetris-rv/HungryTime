using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{
    public BoughtProducts products;
    public void Load()
    {
        products = JsonUtility.FromJson<BoughtProducts>(File.ReadAllText(Application.streamingAssetsPath + "/SaveSkins.json"));
    }
    public void Save()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/SaveSkins.json", JsonUtility.ToJson(products));
    }
    [System.Serializable]
    public class BoughtProducts
    {
        public bool[] isBuy;
    }
}
