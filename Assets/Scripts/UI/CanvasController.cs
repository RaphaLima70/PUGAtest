using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public GameObject endGamePanel, gameOverText, winText;
    public Text coinCollectedText, remainingTimeText;
    public Image lifeBar;
    ShipController shipRef;
    CurrencyManager currencyRef;

    [Header("Behaviour")]
    public float timeCount;
    public float lerpSpeed;
    float percentage;
    float initialHealth;
    float remainingTime;
    float initialTime = 30;


    void Start()
    {
        remainingTime = initialTime;
        shipRef = FindObjectOfType<ShipController>();
        currencyRef = FindObjectOfType<CurrencyManager>();
        initialHealth = shipRef.allStatus[shipRef.healthLevel - 1].health;
    }


    void Update()
    {
        if (GameManager.Instance.endGame && !endGamePanel.active)
        {
            ShowEndGame();
        }
        else
        {
            if (!GameManager.Instance.endGame)
            {
                UpdateGUI();
                remainingTime -= Time.deltaTime;
            }
        }

        if (remainingTime <= 0)
        {
            GameManager.Instance.endGame = true;
        }
    }

    void ShowEndGame()
    {
        endGamePanel.SetActive(true);
        if (GameManager.instance.loose)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    void UpdateGUI()
    {
        float minutes = Mathf.Floor(remainingTime / 60);
        float seconds = Mathf.Floor(remainingTime % 60);
        remainingTimeText.text = (minutes).ToString("00") + ":" + (seconds).ToString("00");
        coinCollectedText.text = "Coins: " + currencyRef.totalCurrencys;
        percentage = ((shipRef.allStatus[shipRef.healthLevel - 1].health * 100) / initialHealth) / 100;
        lifeBar.fillAmount = Mathf.Lerp(lifeBar.fillAmount, percentage, Time.deltaTime * lerpSpeed);
    }

    public void RestartButton()
    {
        endGamePanel.SetActive(false);

        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        currencyRef.SaveCurrency();
        currencyRef.totalCurrencys = 0;
        GameManager.instance.RestatGame();
        remainingTime = initialTime;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Home");
    }
}
