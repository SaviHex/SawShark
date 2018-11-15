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
    public int torpedoKill;
    public int time;
    public int total;

    [Header("GUI")]
    public TextMeshProUGUI fishText;
    public TextMeshProUGUI squidText;
    public TextMeshProUGUI torpedoText;
    public TextMeshProUGUI torpedoKillText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalText;

    private int fishCounter;
    private int squidCounter;
    private int torpedoCounter;
    private int torpedoKillCounter;
    private int timeCounter;
    private int totalCounter;

    private void Awake()
    {
        string serialized = PlayerPrefs.GetString("LastPointStats");
        var lastPointStats = JsonUtility.FromJson<LastPointStats>(serialized);
        
        fish = lastPointStats.Fish;
        squid = lastPointStats.Squid;
        torpedo = lastPointStats.Torpedo;
        torpedoKill = lastPointStats.TorpedoKill;
        time = lastPointStats.Time;
        CalculateTotal();
        lastPointStats.Total = total;

        AddToTotalPlayerStats(lastPointStats);

        fishText.text = fish.ToString();
        squidText.text = squid.ToString();
        torpedoText.text = (torpedo * -1).ToString();
        torpedoKillText.text = torpedoKill.ToString();
        timeText.text = time.ToString();
        
        totalText.text = total.ToString();
    }

    private void CalculateTotal()
    {
        total = (fish + (squid * 2) + (torpedo * -2) + (torpedoKill * 3) + time);
    }

    private void UpdateUI()
    {
        fishText.text = fishCounter.ToString();
        squidText.text = squidCounter.ToString();
        torpedoText.text = (torpedoCounter * -1).ToString();
        torpedoKillText.text = torpedoKillCounter.ToString();
        timeText.text = timeCounter.ToString();
    }

    private void AddToTotalPlayerStats(LastPointStats lastPoints)
    {
        if (!PlayerPrefs.HasKey("PlayerStats"))
        {
            PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(new PlayerStats()));
        }

        string totalStatsSerialized = PlayerPrefs.GetString("PlayerStats");
        var playerStats = JsonUtility.FromJson<PlayerStats>(totalStatsSerialized);

        playerStats.DeathCounter += 1;
        playerStats.TotalFish += lastPoints.Fish;
        playerStats.TotalSquid += lastPoints.Squid;
        playerStats.TotalTorpedo += lastPoints.Torpedo;
        playerStats.TotalTorpedoKill += lastPoints.TorpedoKill;
        playerStats.TotalPoints += lastPoints.Total;
        playerStats.RecordTime = PlayerPrefs.GetInt("HighScore", 0);

        PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(playerStats));
    }
}
