using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  
    [SerializeField]
    private GameObject panelMainMenu;
    [SerializeField]
    private GameObject panelOptions;
    [SerializeField]
    private GameObject panelShop;
    [SerializeField]
    private SaveController _saveController;
    public Shop shop;
    [SerializeField]
    public Text coinsText;

    [SerializeField] private GameObject[] playerSkins;

    private void Awake()
    {
    
        foreach (var skins in playerSkins)
        {
            skins.SetActive(false);
        }
        playerSkins[PlayerPrefs.GetInt("ActiveSkin", 3)].SetActive(true);
    }

    private void Start()
    {
        PlayerPrefs.GetInt("SaveCoins");
        _saveController.Load();
        panelMainMenu.SetActive(true);
        panelOptions.SetActive(false);
        panelShop.SetActive(false);
    }
    public void onPlay()
    {
        StartCoroutine(Play());
    }

    public void onMenuOptions()
    {
        StartCoroutine(Options());
    }
    public void BackToMenu()
    {
        StartCoroutine(BacktoMenu());
    }
    public void onExit()
    {
        StartCoroutine(Exit());
    }

    public void onShop()
    {
        StartCoroutine(Shop());

    }
    IEnumerator Options()
    {
        yield return new WaitForSeconds(0.5f);
        panelOptions.SetActive(true);
        panelMainMenu.SetActive(false);
    }
    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);

    }
    IEnumerator BacktoMenu()
    {
        yield return new WaitForSeconds(0.5f);
        panelOptions.SetActive(false);
        panelMainMenu.SetActive(true);
        panelShop.SetActive(false);
    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    IEnumerator Shop()
    {
        yield return new WaitForSeconds(0.5f);
        panelShop.SetActive(true);
        panelMainMenu.SetActive(false);
        shop.models[0].SetActive(true);
    }
    public void ResetProgress()
    {
        for (int i = 0; i < _saveController.products.isBuy.Length; i++)
        {
            _saveController.products.isBuy[i] = false;
        }
        PlayerPrefs.SetInt("SaveCoins", 0);
        coinsText.text = shop.coins.ToString();
        _saveController.Save();
        panelOptions.SetActive(false);
        panelMainMenu.SetActive(true);
        PlayerPrefs.GetInt("SaveCoins");
        _saveController.Load();
    }

}
