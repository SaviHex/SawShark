using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Models;

public class StatsSummary : MonoBehaviour
{
    [Header("Stats")]
    public int fish;
    public int squid;
    public int torpedo;
    public int time;
    public int total;

    [Header("GUI")]
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI squidText;
    public TextMeshProUGUI torpedoText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalText;

    [Header("Humour")]
    public TextAsset dialogSource;
    public TextMeshProUGUI messageText;

    private int fishCounter;
    private int squidCounter;
    private int torpedoCounter;
    private int timeCounter;
    private int totalCounter;

    private void Awake()
    {
        string serialized = PlayerPrefs.GetString("LastPointStats");
        Debug.Log(serialized);
        var lastPointStats = JsonUtility.FromJson<LastPointStats>(serialized);

        fish = lastPointStats.Fish;
        squid = lastPointStats.Squid;
        torpedo = lastPointStats.Torpedo;
        time = lastPointStats.Time;
        CalculateTotal();
        lastPointStats.Total = total;

        AddToTotalPlayerStats(lastPointStats);
        /*
        fishText.text = fish.ToString();
        squidText.text = squid.ToString();
        torpedoText.text = (torpedo * -1).ToString();
        timeText.text = time.ToString();
        totalText.text = total.ToString();
        */

        StartCoroutine(CountUpFields());
    }

    private void CalculateTotal()
    {
        total = (fish + (squid * 3) + (torpedo * -2) + time);
    }

    private void UpdateUI()
    {
        fishText.text = fishCounter.ToString();
        squidText.text = squidCounter.ToString();
        torpedoText.text = (torpedoCounter * -1).ToString();
        timeText.text = timeCounter.ToString();
        totalText.text = totalCounter.ToString();
    }

    private IEnumerator CountUpFields()
    {        
        float interpolation = 0f;
        while (interpolation < 1f)
        {
            interpolation += 0.1f;

            fishCounter = Counter(interpolation, fish);
            squidCounter = Counter(interpolation, squid);
            torpedoCounter = Counter(interpolation, torpedo);
            timeCounter = Counter(interpolation, time);
            totalCounter = Counter(interpolation, total);
            
            UpdateUI();

            yield return new WaitForSeconds(0.1f);
        }
    }

    private int Counter(float interp, int target)
    {
        return (int)Mathf.Lerp(0, target, interp);
    }

    private void AddToTotalPlayerStats(LastPointStats lastPoints)
    {
        if (!PlayerPrefs.HasKey("PlayerStats"))
        {
            PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(PlayerStats.Instance));
            PlayerPrefs.Save();
            Debug.Log("Novo Player Stats");
        }

        string totalStatsSerialized = PlayerPrefs.GetString("PlayerStats");
        Debug.Log(totalStatsSerialized);
        PlayerStats.Instance = JsonUtility.FromJson<PlayerStats>(totalStatsSerialized);
        bool isNewRecord = (lastPoints.Time > PlayerStats.Instance.RecordTime);

        PlayerStats.Instance.DeathCounter += 1;
        PlayerStats.Instance.TotalFish += lastPoints.Fish;
        PlayerStats.Instance.TotalSquid += lastPoints.Squid;
        PlayerStats.Instance.TotalTorpedo += lastPoints.Torpedo;
        PlayerStats.Instance.TotalPoints += lastPoints.Total;
        PlayerStats.Instance.RecordTime = PlayerPrefs.GetInt("HighScore", 0);

        PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(PlayerStats.Instance));
        PlayerPrefs.Save();

        totalStatsSerialized = PlayerPrefs.GetString("PlayerStats");
        Debug.Log(totalStatsSerialized);

        RandomQuotes rq = new RandomQuotes(dialogSource)
        {
            ignoreEntries = new List<int> { 0, 1, 2, 3 }
        };

        switch (PlayerStats.Instance.DeathCounter)
        {
            case 1:
                messageText.text = rq.entries[0];
                break;
            case 2:
                messageText.text = rq.entries[1];
                break;
            case 3:
                messageText.text = rq.entries[2];
                break;
            default:
                // Let's be funny.        
                if (isNewRecord)
                {
                    messageText.text = "New Record! Good one bro. But you will never beat my 999999 seconds record, ha ha ha!";
                }
                else
                {
                    if (lastPoints.Time > 150)
                    {
                        rq.ignoreEntries.Remove(3);
                    }

                    messageText.text = rq.PickRandomQuote();
                }
                break;
        }

    }
}
