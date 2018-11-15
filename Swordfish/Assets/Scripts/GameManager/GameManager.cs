using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
    [Header("Death Screen")]
    public GameObject deathScreen;  
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
        LastPointStats.Instance.Time = timeLeft;
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        bool newRecord = LastPointStats.Instance.Time > highscore;
        if (newRecord)
        {
            PlayerPrefs.SetInt("HighScore", LastPointStats.Instance.Time);            
            PlayerPrefs.Save();
            highscore = LastPointStats.Instance.Time;
        }

        string serializedLastPointStats = JsonUtility.ToJson(LastPointStats.Instance);
        Debug.Log(serializedLastPointStats);
        PlayerPrefs.SetString("LastPointStats", serializedLastPointStats);
        PlayerPrefs.Save();

        CameraShaker.Instance.ShakeOnce(9f, 10f, .1f, .75f);
        deathScreen.SetActive(true);           
        timeLeft = -100; // Stop CountDown()
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

        var deathAnim = deathScreen.GetComponent<Animator>();        
        yield return new WaitForSeconds(deathAnim.GetCurrentAnimatorStateInfo(0).length);        
        SceneManager.LoadScene("GameOver"); // GameOver...        
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
