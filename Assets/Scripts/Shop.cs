using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] public static Shop _shop;
    [SerializeField]
    public int coins;
    [SerializeField]
    public Text coinsText;
    [SerializeField]
    private GameObject[] btnBuy;
    [SerializeField]
    private SaveController _saveController;
    [SerializeField]
    public GameObject[] models;
    [SerializeField]
    private Text[] priceText;
    [SerializeField]
    private int[] price;
    public int selectModel;
    [SerializeField]
    private GameObject panelShop;
    [SerializeField]
    private GameObject panelMainMenu;
    [SerializeField] GameObject player;

    [SerializeField] private GameObject[] skins;

    private void Awake()
    {
        _saveController.Load();
    }

    private void Start()
    {
        _shop = this;
        coins = PlayerPrefs.GetInt("SaveCoins");
        coinsText.text = coins.ToString();
        for (int i = 0; i < _saveController.products.isBuy.Length; i++)
        {
            if (_saveController.products.isBuy[i] == true)
            {
                priceText[i].text = null;
                btnBuy[i].SetActive(false);
             
            }
        }
    }
    public void BuyProducts(int index)
    {
        if (_saveController.products.isBuy[index] == false)
        {
            if (coins >= price[index])
            {
                btnBuy[index].SetActive(false);
                Debug.Log(btnBuy[index]);
                
                coins -= price[index];

                _saveController.products.isBuy[index] = true;
                _saveController.Save();

                coinsText.text = coins.ToString();
                priceText[index].text = null;
                PlayerPrefs.SetInt("SaveCoins", coins);
            }
        }

    }

    public void LookAndSelect(int index)
    {
        foreach (GameObject m in models)
        {
            m.SetActive(false);
        }
        models[index].SetActive(true);

        if(_saveController.products.isBuy[index] == true)
        {
            selectModel = index;

            foreach (var skin in skins)
            {
                skin.SetActive(false);
            }

            skins[selectModel].SetActive(true);
            PlayerPrefs.SetInt("ActiveSkin", selectModel);
           
        }
    }

    public void BackToMenu()
    {
        StartCoroutine(BacktoMenu());
    }
    IEnumerator BacktoMenu()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < models.Length; i++)
        models[i].SetActive(false);
        panelMainMenu.SetActive(true);
        panelShop.SetActive(false);
        //player.SetActive(true);
    }
}
