using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Text CoinText;

    void Start()
    {
        CoinText.text = "Total Coins: " + PlayerPrefs.GetInt("Currencys", 0); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
