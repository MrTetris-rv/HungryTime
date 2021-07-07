using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    static public MenuGame instance;
    [SerializeField]
    private GameObject Pause;
    public PlayerController[] player;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Pause.SetActive(false);
    }

    public void PauseMenu()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void onMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("SaveCoins", player[PlayerPrefs.GetInt("ActiveSkin", 3)].coins);
        Debug.Log(player[PlayerPrefs.GetInt("ActiveSkin", 3)].coins);
        Pause.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void onContinue()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }
}

   
