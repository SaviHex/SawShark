using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Death Screen")]
    public GameObject deathScreen;
    public Text scoreText;
    public Text messageText;
    public GameObject newRecordText;
    [Range(0.1f, 0.9f)]
    public float slowDownSpeed;

    [Header("Stamina Bar")]    
    public Animator staminaAnim;

    [Header("Timer")]
    public int timeLeft;
    public GameObject uiTimer;
    private IEnumerator timer;

    [Header("Debug")]
    public bool resetPlayerPrefab;

    private void Start()
    {        
        deathScreen.SetActive(false);

        timer = CountDown();        
        StartCoroutine(timer);

        if (resetPlayerPrefab)
            PlayerPrefs.DeleteAll();
    }

    public void GameOver()
    {
        // Saves the highscore.        
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        bool newRecord = timeLeft > highscore;
        if (newRecord)
        {
            PlayerPrefs.SetInt("HighScore", timeLeft);
            PlayerPrefs.Save();
            highscore = timeLeft;
        }

        deathScreen.SetActive(true);
        scoreText.text += timeLeft;

        newRecordText.SetActive(newRecord);

        messageText.text = "I don't know what to say here. You're dead. That's it, man... It's over now. You can go back to the menu and play the game again instead of reading this uninteresting piece of text.";
        
        StopCoroutine(timer);
        //StartCoroutine(SlowDown());
    }

    private IEnumerator CountDown()
    {
        Text[] texts = uiTimer.GetComponentsInChildren<Text>();

        while(timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            //timeLeft--;
            timeLeft++;
            foreach (Text t in texts)
            {
                t.text = timeLeft.ToString();
            }            
        }

        Debug.Log("Next Level!");
    }

    private IEnumerator SlowDown()
    {
        while (Time.timeScale > 0.2)
        {
            Time.timeScale *= slowDownSpeed;
            Debug.Log(Time.timeScale);
            yield return new WaitForSeconds(0.1f);
        }

        Time.timeScale = 0.01f;
    }

    public void ShakeStamina()
    {
        staminaAnim.SetTrigger("Shake");
    }
}
